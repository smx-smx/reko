# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2022

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop';"]

RUN `
    # Install Chocolatey `
    Set-ExecutionPolicy Bypass -Scope Process -Force; `
    Invoke-Expression ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1')); `
    # Install VS Build Tools with VCTools + CMake `
    Invoke-WebRequest -Uri https://aka.ms/vs/17/release/vs_buildtools.exe -OutFile vs_buildtools.exe; `
    Start-Process -Wait -FilePath vs_buildtools.exe -ArgumentList '--quiet', '--wait', '--norestart', '--nocache', `
        '--add', 'Microsoft.VisualStudio.Workload.VCTools', `
        '--add', 'Microsoft.VisualStudio.Component.VC.CMake.Project', `
        '--add', 'Microsoft.VisualStudio.Component.Windows11SDK.26100'; `
    Remove-Item vs_buildtools.exe; `
    # CMake (standalone install so it's on PATH) `
    choco install -y cmake --installargs 'ADD_CMAKE_TO_PATH=System' --no-progress --limit-output; `
    # Reko dependencies `
    choco install -y dotnet-6.0-sdk --no-progress --limit-output; `
    choco install -y dotnet-8.0-sdk --no-progress --limit-output; `
    REG ADD 'HKLM\SOFTWARE\WOW6432Node\Microsoft\NET Framework Setup\NDP\v3.5' /t REG_DWORD /v Install /d 1; `
    REG ADD 'HKLM\SOFTWARE\WOW6432Node\Microsoft\NET Framework Setup\NDP\v3.5' /t REG_DWORD /v SP /d 1; `
    REG ADD 'HKLM\SOFTWARE\WOW6432Node\Microsoft\NET Framework Setup\NDP\v3.5' /t REG_SZ /v Version /d 3.5.30729.4926; `
    choco install -y wixtoolset --no-progress --limit-output; `
    choco install -y git --no-progress --limit-output; `
    choco install -y 7zip --no-progress --limit-output; `
    Remove-Item $env:TEMP\* -Recurse -Force -ErrorAction SilentlyContinue; `
    & $Env:ProgramFiles\dotnet\dotnet nuget locals all --clear; `
    # reserved for cache eviction: 0