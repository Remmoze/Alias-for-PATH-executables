# Alias for PATH executables
This tool allows you to add programs to PATH but under a different program name.

# Usage
Example code:
```
alias add photos "./My Favorite Photo Viewer.exe"
```
From now on you can access this executable the same way as any executable added in PATH

You can even use it as a shortcut in `Run` window  (`win+R`)

![Run dialoge box](https://i.imgur.com/KSLJMMc.png)

# Notes
This program modifies Registry keys at

`HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\`
