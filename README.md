# SmartCardCertExtractor

A tool designed to pull pem and pfx certifications from smart cards. Developed for hardened environments where Smart Card usage is common and tools like cmd, powershell, and mmc are not available (kiosk or hardened workstation breakout).

## Compiling yourself instructions

```sh
git clone https://github.com/ZumiYumi/SmartCardCertExtractor
```
* Open the project in Visual Studio Code (I used 2022)
* Make sure the SDK for NET9.0 is installed
* I used this when compiling to make it a standalone binary (no dlls)

```sh
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true /p:PublishTrimmed=true /p:TrimMode=link -o ./publish
```

## Running it
* Ensure a smartcard is plugged in
* Double left click the binary and it should run in the current directory
* If you have macros or file explorer you can try to run it that way as well
* Will save a pem file (base64 cert) and if it is able to, it will first attempt to extract the private key, if not possible it will attempt to resign the cert with the password given (as long as it matches, if you know the pin)
<img width="794" height="460" alt="image" src="https://github.com/user-attachments/assets/6220fef6-d52a-4f88-9d47-bfd1d16df58f" />
