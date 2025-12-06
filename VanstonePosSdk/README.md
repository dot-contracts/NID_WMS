# Vanstone POS SDK Binding for .NET MAUI

This project is a Xamarin Android binding library for the Vanstone A90 POS device SDK. It enables .NET MAUI Android applications to interact with the Vanstone A90 POS hardware using C# code.

## Contents

- Binding for Vanstone A90 POS device SDK (vanstoneSdkClient-noemv_20231222.jar and AppSdkAidl_buildBy_20231222.jar)
- Native libraries for armeabi-v7a architecture

## Usage

1. Add a reference to this project in your MAUI Android application.
2. Use the generated C# classes to interact with the Vanstone A90 POS device.

### Sample Code

The project includes a `VanstoneSdkHelper` class in the Additions folder that demonstrates how to use the SDK using Java reflection to ensure compatibility:

```csharp
// Initialize the helper 
var helper = new VanstoneSdkHelper(Android.App.Application.Context);

// Get the device serial number
string serialNumber = helper.GetDeviceSerialNumber();

// Print a receipt
bool printSuccess = helper.PrintText("Hello from MAUI!");
```

### Using Java Reflection

The helper class uses Java reflection to interact with the SDK, which ensures compatibility even if some class or method names change. This approach:

1. Finds the SDK engine class using its fully qualified name
2. Gets the singleton instance using reflection
3. Calls methods like printText, getSystemHandler, etc. through reflection
4. Handles any exceptions that might occur during the reflection process

This approach is more resilient to SDK changes and binding issues compared to direct API calls.

## Documentation

The documentation for the original SDK is available in the Documentation folder:
- 《Vanstone Android POS API programming manual v2.00》.docx.pdf

## Project Structure

- `Jars/`: Contains the JAR files from the Vanstone SDK
- `libs/`: Contains the native libraries (.so files)
- `Transforms/`: XML transform files for adapting the Java APIs to C#
- `Additions/`: Contains additional C# code to enhance the generated binding

## Building

This project is set up to be built as part of the main solution. The binding code will be generated during the build process.

## Generated Code

After building, the generated C# binding code will be available in `obj/Debug/net8.0-android/generated/src`.

## Troubleshooting

If you encounter issues with the Vanstone POS device:

1. Ensure the device is properly connected and powered
2. Check if the SDK initialization is successful
3. Refer to the SDK documentation for specific API usage details
4. Make sure you have the necessary permissions in your application's AndroidManifest.xml

### Common Issues

- **Method not found exceptions**: If you encounter method not found exceptions, check the SDK documentation for the correct method names. The Java reflection approach in VanstoneSdkHelper can be adjusted to match the actual method names.
- **Class not found exceptions**: Ensure the JAR files are correctly included in the project and that the binding generation process completed successfully.
- **Native library issues**: Make sure the .so files are correctly included and that your application has the appropriate architecture support. 