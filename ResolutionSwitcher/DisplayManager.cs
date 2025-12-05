using System.Runtime.InteropServices;

namespace ResolutionSwitcher;

public static class DisplayManager
{
    private const int ENUM_CURRENT_SETTINGS = -1;
    private const int CDS_UPDATEREGISTRY = 0x01;
    private const int CDS_TEST = 0x02;
    private const int DISP_CHANGE_SUCCESSFUL = 0;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct DEVMODE
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmFormName;
        public short dmLogPixels;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    }

    [DllImport("user32.dll", CharSet = CharSet.Ansi)]
    private static extern bool EnumDisplaySettingsA(string? deviceName, int modeNum, ref DEVMODE devMode);

    [DllImport("user32.dll", CharSet = CharSet.Ansi)]
    private static extern int ChangeDisplaySettingsA(ref DEVMODE devMode, int flags);

    public static (bool success, string message) ChangeResolution(int width, int height)
    {
        DEVMODE devMode = new DEVMODE();
        devMode.dmSize = (short)Marshal.SizeOf(devMode);

        if (!EnumDisplaySettingsA(null, ENUM_CURRENT_SETTINGS, ref devMode))
        {
            return (false, "EnumDisplaySettings failed");
        }

        int currentWidth = devMode.dmPelsWidth;
        int currentHeight = devMode.dmPelsHeight;

        devMode.dmPelsWidth = width;
        devMode.dmPelsHeight = height;

        int result = ChangeDisplaySettingsA(ref devMode, CDS_TEST);

        if (result != DISP_CHANGE_SUCCESSFUL)
        {
            string errorMessage = result switch
            {
                -1 => $"{width}x{height} is not supported by this monitor",
                -2 => $"{width}x{height} is not supported by this monitor",
                -3 => "Another application is using the display",
                -4 => "Failed to change resolution",
                -5 => "Failed to write to registry",
                -6 => "Restart required",
                -7 => "Display driver error",
                _ => $"Failed to change resolution (Error: {result})"
            };
            return (false, errorMessage);
        }

        result = ChangeDisplaySettingsA(ref devMode, CDS_UPDATEREGISTRY);

        if (result == DISP_CHANGE_SUCCESSFUL)
        {
            return (true, $"Changed to {width}x{height}");
        }
        else
        {
            string errorMessage = result switch
            {
                -1 => $"{width}x{height} is not supported by this monitor",
                -2 => $"{width}x{height} is not supported by this monitor",
                -3 => "Another application is using the display",
                -4 => "Failed to change resolution",
                -5 => "Failed to write to registry",
                -6 => "Restart required",
                -7 => "Display driver error",
                _ => $"Failed to change resolution (Error: {result})"
            };
            return (false, errorMessage);
        }
    }
}
