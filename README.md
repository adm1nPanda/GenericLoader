# GenericLoader

## Command to execute

`./GenericLoader.exe <PATH TO B64 File>`


## How it works

1. Decodes bytes from Base64 string
2. Executes bytes in memory
3. Redirects STDOUT into a string, to forward output to c2.

## Create Base64 string

Run powershell command -
`[System.Convert]::ToBase64String([System.IO.File]::ReadAllBytes(".\Path\To\sample.exe")) > sample.b64`


## Similar Projects

1. https://github.com/malcomvetter/ManagedInjection
