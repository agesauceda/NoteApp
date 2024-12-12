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
    private List<string> _audioBase64List = new List<string>();

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
        this._controller = new VoicePageController((VoicePageViewInterface)this);
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
                if (_audioBase64List.Count < _maxAudios)
                {
                    _audioBase64List.Add(audioBase64);
                    await DisplayAlert("Grabación", "Audio agregado correctamente.", "OK");
                    UpdateAudioCarousel();
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

    private void UpdateAudioCarousel()
    {
        audioCarousel.ItemsSource = _audioBase64List
            .Select(audioBase64 => $"data:audio/mp4;base64,{audioBase64}")
            .ToList();
    }

    private async void OnSaveNoteClicked(object sender, EventArgs e)
    {
        if (Validator.ValidateString(txtTitle.Text) && Validator.ValidateString(txtDescription.Text))
        {
        }

        if (_audioBase64List.Count == 0)
        {
            await DisplayAlert("Error", "Debes grabar al menos un audio antes de guardar.", "OK");
            return;
        }

        var note = new NoteVoicePOST
        {
            titulo = txtTitle.Text, 
            descripcion = txtDescription.Text,
            audio = _audioBase64List
        };

        Console.WriteLine("Título: " + note.titulo);
        Console.WriteLine("Descripción: " + note.descripcion);
        Console.WriteLine("Audios en Base64:");
        foreach (var audio in note.audio)
        {
            Console.WriteLine(audio);
        }

        await _controller.RegisterNoteVoice(note);
        await DisplayAlert("Éxito", "Nota de voz guardada correctamente.", "OK");
        _audioBase64List.Clear();
        UpdateAudioCarousel();
    }

    public async Task RegisterNoteVoice(string msg)
    {
        await DisplayAlert("Registro de Nota", msg, "Aceptar");
    }
}