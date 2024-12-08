using System.Timers;
using System;

namespace NoteApp.Views;

public partial class VoicePage : ContentPage
{
    private bool _isPlaying = false;
    private bool _isRecording = false;
    private System.Timers.Timer _waveUpdateTimer;
    private Random _random = new Random();

    public VoicePage()
	{
		InitializeComponent();
	}
    private void OnRecordAudioClicked(object sender, EventArgs e)
    {
        if (_isRecording)
        {
            _isRecording = false;
            _waveUpdateTimer?.Stop();

            RecordButton.BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#808080");
        }
        else
        {
            _isRecording = true;
            if (_waveUpdateTimer == null)
            {
                _waveUpdateTimer = new System.Timers.Timer(200);
                _waveUpdateTimer.Elapsed += UpdateSoundWave;
            }
            _waveUpdateTimer.Start();

            RecordButton.BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#3D5AFE");
        }
    }
    private void UpdateSoundWave(object sender, ElapsedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            foreach (var child in SoundWaveContainer.Children)
            {
                if (child is BoxView boxView)
                {

                    boxView.HeightRequest = _random.Next(10, 60);
                }
            }
        });
    }

    private void OnPlayButtonClicked(object sender, EventArgs e)
    {
        if (_isPlaying)
        {

            PlayButton.BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#808080");
            _isPlaying = false;
        }
        else
        {

            PlayButton.BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#3D5AFE");
            _isPlaying = true;
        }
    }
}