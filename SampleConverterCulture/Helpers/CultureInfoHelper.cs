using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace SampleConverterCulture.Helpers;

/// <summary>
/// This static class is implementing logic to set the culture globally.
/// </summary>
public static class CultureInfoHelper
{
	/// <summary>
	/// This method is setting the app's culture information.
	/// <para></para>
	/// The culture information is needed to display date or number values 
	/// according to the cultures format style.
	/// <para></para>
	/// <br></br>- 
	/// https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.binding.converterculture?view=windowsdesktop-8.0
	/// <br></br>- 
	/// If this property is not set, the binding engine uses the Language property of the binding target object.
	/// In XAML this defaults to "en-US" or inherits the value from the root element (or any element) of the page, 
	/// if one has been explicitly set.
	/// </summary>
	/// <param name="cultureInfo">The standard culture is read from <see cref="CultureInfo.CurrentCulture"/>.</param>
	public static void Set(CultureInfo? cultureInfo = null)
	{
		// Validate parameters.
		if (cultureInfo is null)
		{
			cultureInfo = CultureInfo.CurrentCulture;
		};

		Thread.CurrentThread.CurrentCulture = cultureInfo;
		Thread.CurrentThread.CurrentUICulture = cultureInfo;

		OverrideMetadata(cultureInfo, false);

		// TODO - Create a custom binding class (see CustomBinding.cs) that automatically sets the ConverterCulture to the current culture when created.
	}

	/// <summary>
	/// This work around is needed to activate the culture in WPF controls like TextBox.
	/// <para></para>
	/// In some cases the WPF controls do not read from <see cref="CultureInfo.CurrentCulture"/> or
	/// <see cref="CultureInfo.CurrentUICulture"/> properties. To make sure, all WPF controls have
	/// enabled the user's settings the language propery of the framework element needs to be set.
	/// </summary>
	/// <param name="cultureInfo"></param>
	/// <param name="enable">set to false, to ignore this function</param>
	private static void OverrideMetadata(CultureInfo cultureInfo, bool enable = true)
	{
		if (enable)
		{
			FrameworkElement.LanguageProperty.OverrideMetadata(
				forType: typeof(FrameworkElement),
				typeMetadata: new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(cultureInfo.Name)));
		}
	}
}
