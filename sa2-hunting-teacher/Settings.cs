using System.Text.Json;

namespace sa2_hunting_teacher;

internal class Settings {
	/// <summary>
	/// The default value of the Reversed Hints checkbox that appears for Mad Space
	/// When true (i.e. checked), the hints in Mad Space will be reversed (i.e. vanilla behavior)
	/// When false (i.e. not checked), the hints in Mad Space will not be reversed (i.e. the hints will appear left to right readable)
	/// Defaults to <c>false</c>.
	/// </summary>
	public bool MspReversedHints { get; set; } = false;
	public bool BackToMenu { get; set; } = false;

	public static readonly string AppDataPath = Path.Combine(
		System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
		"SA2 Hunting Teacher"
	);

	public static readonly string SettingsPath = Path.Combine(
		Settings.AppDataPath,
		"settings.json"
	);

	private static readonly JsonSerializerOptions JSONOptions = new() {
		PropertyNameCaseInsensitive = false,
		ReadCommentHandling = JsonCommentHandling.Skip,
		AllowTrailingCommas = true,
		WriteIndented = true
	};

	[System.Obsolete("Do not use this constructor directly. Use Settings.Load() instead.", false)]
	public Settings() { }

	public static Settings Load() {
#pragma warning disable CS0618
		if (!Path.Exists(Settings.AppDataPath)) {
			Directory.CreateDirectory(Settings.AppDataPath);
			return new Settings();
		}

		if (!Path.Exists(Settings.SettingsPath)) {
			return new Settings();
		}

		return JsonSerializer.Deserialize<Settings>(
			File.ReadAllText(Settings.SettingsPath),
			Settings.JSONOptions
		) ?? new Settings();
#pragma warning restore CS0618
	}

	public void Save() {
		File.WriteAllText(
			Settings.SettingsPath,
			JsonSerializer.Serialize<Settings>(this, Settings.JSONOptions)
		);
	}
}
