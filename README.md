# GenericLoader

Use tool to execute shellcode in memory and redirect STDOUT to a stream which can be read by a C2 server.

## Command to execute

`./GenericLoader.exe <PATH TO B64 File>`


## Program execution steps

1. Redirects STDOUT into stream.
2. Decodes bytes from Base64 string passed via cmd arguments
3. Executes bytes in memory
4. Returns STDOUT of executed program as a string.

## Create Base64 string

Powershell OneLiner -

`[System.Convert]::ToBase64String([System.IO.File]::ReadAllBytes(".\Path\To\sample.exe")) > sample.b64`

C# Code

`// Code Block to B64 encode contents of a exe.
byte[] Filecont = System.IO.File.ReadAllBytes(args[0]);
string B64Filecont = Convert.ToBase64String(Filecont,0,Filecont.Length);`



## Similar Projects

1. https://github.com/malcomvetter/ManagedInjection
