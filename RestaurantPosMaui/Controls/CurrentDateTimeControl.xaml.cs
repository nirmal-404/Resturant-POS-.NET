namespace RestaurantPosMaui.Controls;

public partial class CurrentDateTimeControl : ContentView, IDisposable
{
	private readonly PeriodicTimer _timer;
	public CurrentDateTimeControl()
	{
		InitializeComponent();

		dayTimeLbl.Text = DateTime.Now.ToString("dddd, hh:mm:ss tt");
		dateLabel.Text = DateTime.Now.ToString("MMM dd, yyyy");

		_timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
		UpdateTimer();
    }

	public void Dispose() => _timer?.Dispose();

	private async void UpdateTimer()
	{
		while (await _timer.WaitForNextTickAsync())
		{ 
			dayTimeLbl.Text = DateTime.Now.ToString("dddd, hh:mm:ss tt");
			dateLabel.Text = DateTime.Now.ToString("MMM dd, yyyy");
		}
	}
}