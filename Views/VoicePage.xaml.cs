using System.Timers;
using System;
using NoteApp.Views.Interfaces;
using NoteApp.Controllers.VoicePage;
using NoteApp.Models.VoicePage;
using Plugin.Firebase.CloudMessaging;
using NoteApp.Utils;

#if ANDROID
using Android.Media; 
#endif

namespace NoteApp.Views;

public partial class VoicePage : ContentPage, VoicePageViewInterface
{
    private readonly VoicePageController _controller;
    private NoteVoicePOST _voicePost;
    private List<string> _audios;

    private string AudioBase;
    private int _maxAudios = 3; 
    private string _audioFilePath; 
    private bool _isRecording; 
    #if ANDROID
    private MediaRecorder _recorder; 
    #endif

    public VoicePage()
	{
		InitializeComponent();
        _audios = new List<string>();
        _controller = new VoicePageController((VoicePageViewInterface)this);
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await getTokenFCM();
    }

    private async Task getTokenFCM()
    {
        try
        {
            await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
            var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
            Preferences.Set("tokenFCM", token);
            Console.WriteLine(Preferences.Get("tokenFCM", ""));
        }
        catch (Exception e)
        {
            await DisplayAlert("", e.Message, "Ok");
        }
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
                    string tempAudioFilePath = await ConvertBase64ToTempFile(audioBase64);

                    CarouselObject.AddAudio(tempAudioFilePath);
                    await DisplayAlert("Grabación", "Audio agregado correctamente.", "OK");
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
    #endif
    }
    private async Task<string> ConvertBase64ToTempFile(string base64Audio)
    {
        string tempPath = Path.Combine(FileSystem.CacheDirectory, $"audio_{Guid.NewGuid()}.mp4");
        byte[] audioBytes = Convert.FromBase64String(base64Audio);
        await File.WriteAllBytesAsync(tempPath, audioBytes);
        return tempPath;
    }

    private async void OnSaveNoteClicked(object sender, EventArgs e)
    {
        if (Validator.ValidateString(txtTitle.Text) && Validator.ValidateString(txtDescription.Text))
        {
            var list = CarouselObject.GetAudios();
            for (int i = 0; i < list.Count; i++)
            {
                _audios.Add(Converters.ConvertToBase64(list[i]).Result);
            }
            _voicePost = new NoteVoicePOST
            {
                titulo = txtTitle.Text,
                descripcion = txtDescription.Text,
                audio = _audios
            };
            await _controller.RegisterNoteVoice( _voicePost );
            flushData();
        }
    }
    private void flushData()
    {
        _voicePost = new NoteVoicePOST();
        txtDescription.Text = "";
        txtTitle.Text = "";
        _audios.Clear();
        CarouselObject.ClearAudios();
    }

    public async Task RegisterNoteVoice(string msg)
    {
        await DisplayAlert("Registro de Nota", msg, "Aceptar");
    }

    public Task GetAudio(List<AudiosNoteGET> voices)
    {
        return Task.CompletedTask;
    }

    public Task UpdateNoteVoice(string msg)
    {
        return Task.CompletedTask;
    }
}