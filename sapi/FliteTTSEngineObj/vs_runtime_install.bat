# Check VCRedist current version
$OS= if ( ${env:ProgramFiles(x86)} ) {"\WOW6432Node"} else {"\"}
    $vcredist = Get-ItemProperty -Path "HKLM:\SOFTWARE$OS\Microsoft\VisualStudio\14.0\VC\Runtimes\x86" -ErrorAction SilentlyContinue -ErrorVariable eVcRedist
if (! $eVcRedist or $vcredist.Bld -le 24215) {
    vc_redist.x86.exe
}
else {
	$Warning += @( "Test Test Test" )
}
