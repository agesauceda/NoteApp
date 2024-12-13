using NoteApp.Controllers.VoicePage;
using NoteApp.Models.Dashboard;
using NoteApp.Models.VoicePage;
using NoteApp.Utils;
using NoteApp.Views.Interfaces;

#if ANDROID
using Android.Media; 
#endif

namespace NoteApp.Views;

public partial class VoicePageUpdate : ContentPage, VoicePageViewInterface
{
    private AudioNotePUT put;
    private List<AudioPOST> _audios;
    private readonly VoicePageController _controller;
    private ObjectDashBoard obj;

    private string AudioBase;
    private int _maxAudios = 3;
    private string _audioFilePath;
    private bool _isRecording;
#if ANDROID
    private MediaRecorder _recorder; 
#endif
    public VoicePageUpdate(ObjectDashBoard dashBoard)
	{
		InitializeComponent();
        _audios = new List<AudioPOST>();
        obj = dashBoard;
        _controller = new VoicePageController((VoicePageViewInterface)this);
	}
   
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        txtTitle.Text = obj.titulo;
        txtDescription.Text = obj.contenido;
        await _controller.GetAudios(obj.id.Value);
    }

    private async void OnRecordAudioClicked(object sender, EventArgs e)
    {
#if ANDROID
        bool hasMicrophonePermission = await RequestPermissions.CheckPermissionMicrophoneAsync();
        if (!hasMicrophonePermission)
        {
            await DisplayAlert("Error", "Permiso de micrófono denegado.", "OK");
            return;
        }

        if (_isRecording)
        {
            _recorder.Stop();
            _recorder.Release();
            _recorder.Dispose();
            _recorder = null;

            _isRecording = false;

             var audioBase64 = await Converters.ConvertAudioToBase64(_audioFilePath);

            if (audioBase64 != null)
            {
                if (_audios.Count < _maxAudios)
                {
                    var newAudio = new AudioPOST
                    {
                        id = 0, 
                        audio = audioBase64
                     };
                     _audios.Add(newAudio);

                    string tempAudioFilePath = await ConvertBase64ToTempFile(audioBase64);
                    await DisplayAlert("Grabación", "Audio agregado correctamente.", "OK");
                    CarouselObject.AddAudio(0, tempAudioFilePath);

                }
                else
                {
                    await DisplayAlert("Límite alcanzado", "Solo puedes grabar hasta 3 audios.", "OK");
                }
            }
        }
        else
        {
            _audioFilePath = Path.Combine(FileSystem.AppDataDirectory, $"audio_{DateTime.Now:yyyyMMddHHmmss}.mp4");

            _recorder = new MediaRecorder();
            _recorder.SetAudioSource(AudioSource.Mic);
            _recorder.SetOutputFormat(OutputFormat.Mpeg4);
            _recorder.SetAudioEncoder(AudioEncoder.Aac);
            _recorder.SetOutputFile(_audioFilePath);

            _recorder.Prepare();
            _recorder.Start();
            _isRecording = true;

            await DisplayAlert("Grabación", "Grabando audio...", "OK");
        }
#else
        await DisplayAlert("Error", "Grabación no soportada en esta plataforma.", "OK");

#endif
    }

    private async void OnUpdateNoteClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtDescription.Text))
        {
            await DisplayAlert("Error", "El título y la descripción no pueden estar vacíos.", "OK");
            return;
        }

        put = new AudioNotePUT
        {
            titulo = txtTitle.Text,
            descripcion = txtDescription.Text,
            audio = _audios
        };

        if (_audios == null || !_audios.Any())
        {
            await DisplayAlert("Error", "Debes agregar al menos un audio antes de actualizar.", "OK");
            return;
        }
        await _controller.UpdateNoteVoice(put, obj.id.Value);
        flushData();
    }

    private void flushData()
    {
        put = new AudioNotePUT();
        txtTitle.Text = "";
        txtDescription.Text = "";
        _audios.Clear();
        CarouselObject.ClearAudios();

    }

    public async Task GetAudio(List<AudiosNoteGET> voices)
    {
        if (voices != null) 
        {
            for (int i = 0; i < voices.Count; i++) 
            {   
               var path = await Converters.ConvertBase64ToFileAudio(voices[i].base64, voices[i].id.Value);
                CarouselObject.AddAudio(voices[i].id.Value, path);
            }
        }
    }
    private async Task<string> ConvertBase64ToTempFile(string base64Audio)
    {
        string tempPath = Path.Combine(FileSystem.CacheDirectory, $"audio_{Guid.NewGuid()}.mp4");
        byte[] audioBytes = Convert.FromBase64String(base64Audio);
        await File.WriteAllBytesAsync(tempPath, audioBytes);
        return tempPath;
    }

    public async Task RegisterNoteVoice(string msg)
    {
        
    }

    public async Task UpdateNoteVoice(string msg)
    {
        await DisplayAlert("Actualización de Nota", msg, "Aceptar");
    }
}