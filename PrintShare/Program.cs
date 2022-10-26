using System.Management;
using System.Security.Principal;

using (ManagementClass win32Printer = new ManagementClass("Win32_Printer"))
{
    using (ManagementBaseObject inputParam =
       win32Printer.GetMethodParameters("AddPrinterConnection"))
    {
        // Replace <server_name> and <printer_name> with the actual server and
        // printer names.
        inputParam.SetPropertyValue("Name", "\\\\REDACTED");

        using (ManagementBaseObject result =
            (ManagementBaseObject)win32Printer.InvokeMethod("AddPrinterConnection", inputParam, null))
        {
            uint errorCode = (uint)result.Properties["returnValue"].Value;

            switch (errorCode)
            {
                case 0:
                    Console.Out.WriteLine("Successfully connected printer.");
                    break;
                case 5:
                    Console.Out.WriteLine("Access Denied.");
                    break;
                case 123:
                    Console.Out.WriteLine("The filename, directory name, or volume label syntax is incorrect.");
                    break;
                case 1801:
                    Console.Out.WriteLine("Invalid Printer Name.");
                    break;
                case 1930:
                    Console.Out.WriteLine("Incompatible Printer Driver.");
                    break;
                case 3019:
                    Console.Out.WriteLine("The specified printer driver was not found on the system and needs to be downloaded.");
                    break;
            }
        }
        
        Console.Write("Please wait, configuring second printer...");

        inputParam.SetPropertyValue("Name", "\\\\REDACTED");

        using (ManagementBaseObject result =
            (ManagementBaseObject)win32Printer.InvokeMethod("AddPrinterConnection", inputParam, null))
        {
#pragma warning disable CA1416 // Validate platform compatibility
            uint errorCode = (uint)result.Properties["returnValue"].Value;
#pragma warning restore CA1416 // Validate platform compatibility

            switch (errorCode)
            {
                case 0:
                    Console.Out.WriteLine("Successfully connected second printer. You may now close this window");
                    break;
                case 5:
                    Console.Out.WriteLine("Access Denied.");
                    break;
                case 123:
                    Console.Out.WriteLine("The filename, directory name, or volume label syntax is incorrect.");
                    break;
                case 1801:
                    Console.Out.WriteLine("Invalid Printer Name.");
                    break;
                case 1930:
                    Console.Out.WriteLine("Incompatible Printer Driver.");
                    break;
                case 3019:
                    Console.Out.WriteLine("The specified printer driver was not found on the system and needs to be downloaded.");
                    break;
            }
        }
       
    }
}
