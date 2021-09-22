#Could not find a part of the path … bin\roslyn\csc.exe

##Option1
I followed these steps and it worked perfectly

Delete all the bin and obj folders
Clean solution and rebuild
Run this command in powershell
Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r

You need to install Microsoft.CodeDom.Providers.DotNetCompilerPlatform.BinFix, was especially created for that error

the system.codedom node, you can see why it was bringing in roslyn: compilerOptions="/langversion:6

I had the same problem when installing my application on the server when everything worked perfectly on localhost.

None of these solutions woorked, I always had the same error:

##Option2
Could not find a part of the path 'C:\inetpub\wwwroot\myApp\bin\roslyn\csc.exe'
I ended up doing this:

on my setup project, right clic, view > file system
create a bin/roslyn folder
select add > files and add all files from packages\Microsoft.Net.Compilers.1.3.2\tools
This solved my problem.

##Option3

This was my problem - the bin/roslyn folder is there when a project is created, however, if you delete it or, like source control, it is not copied, then it doesn't get rebuilt. I think there is a bit of a "sync" issue with the versions, once 1.0.1 is installed and update the Import in proj file to correct version, a build will then automatically copy the Roslyn folder - no need for any of those post build commands. –


# Nuget
Force Nuget to Reinstall Packages without Updating
Occasionally I run into an issue where I’ll open a solution in Visual Studio, build it, and the build will fail because of dependent packages. I’ll try every way offered by Visual Studio to restore packages, but it will claim everything is up to date. Looking in Solution Explorer, you’ll see that some packages are clearly missing (icons on the packages showing they’re not there), but no amount of telling VS to restore packages (or building, which should do the restore as well) will get them.

The fix for this is to open Package Manager Console and run this command:
`Update-Package -reinstall`
Note: If you just run Update-Package, it will try to update all packages to the latest version, which isn’t necessarily what you want (especially if you’ve simply pulled from source control and want the project to just build with the versions of packages it has in source control).