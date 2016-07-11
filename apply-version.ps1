# This script update version.
Param(
	[string] [Parameter(Mandatory=$true)] $Version
)

$exitCode = 0
$projects = Get-ChildItem .\src | ?{$_.PsIsContainer} | ?{$_.Name.Contains("Core")}
foreach($project in $projects)
{
	$projectPath = $project.FullName
	$projectName = $project.Name

	Write-Host "Updating version of $projectName to $Version ..." -ForegroundColor Green

	$projectJson = Get-Content -Path $projectPath\project.json | ConvertFrom-Json
	$projectJson.version = $Version
	$projectJson | ConvertTo-Json -Depth 999 | Out-File -FilePath $projectPath\project.json -Encoding utf8

	Write-Host "Updating version of $projectName to $Version success" -ForegroundColor Green

	$exitCode += $LASTEXITCODE
}

if($exitCode -ne 0) {
	$host.SetShouldExit($exitCode)
}
