---------------------------------
NuGetRunner.exe
---------------------------------

Read the documentation for more information.
https://github.com/theonlylawislove/NuGetRunner

---------------------------------
Sample Xml
---------------------------------

<?xml version="1.0" encoding="utf-8" ?>
<Packages>
  <PreActions>
    <Command>xcopy /f/i "$(rootDirectory)/test.txt" "$(rootDirectory)/$(version)/</Command>
    <PowerShell>$(rootDirectory)\build.ps1</PowerShell>
  </PreActions>
  <Package name="Package1">
    <PreActions>
      <Command>$(rootDirectory)\ILMerge.bat</Command>
    </PreActions>
    <NuSpecPath>$(sourcesDirectory)\Package1.nuspec</NuSpecPath>
    <OutputDirectory>$(rootDirectory)\Package</OutputDirectory>
    <BasePath>$(rootDirectory)\BuildOutputs</BasePath>
    <Deployments>
      <Deployment name="NuGetGallery" path="http://nuget.org" apiKey="secret..." />
      <Deployment name="LocalFilePath" path="$(packagesDirectory)" />
    </Deployments>
    <PostActions>
      <Command>$(rootDirectory)\cleanup.bat</Command>
    </PostActions>
  </Package>
  <PostActions></PostActions>
</Packages>

-----------------------------------
Sample Arguements
-----------------------------------

NuGetRunner.exe /n "C:\NuGet.exe" /d "Definitions.xml" /v "1.2"