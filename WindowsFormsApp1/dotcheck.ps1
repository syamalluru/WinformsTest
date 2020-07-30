
$message=''
$errorrs=''

#Get the User's Operating system version

 if ((gwmi win32_operatingsystem | select osarchitecture).osarchitecture -eq "64-bit")
{
    #64 bit logic here
    $message+='Operating System : 64-bit'|Out-String
}
else
{
    #32 bit logic here
   $errorrs+='Operating System : 32-bit'|Out-String
}

# Function to get Key and properties from registery.
function Get-KeyPropertyValue($key, $property)
{
    if($key.Property -contains $property)
    {
        Get-ItemProperty $key.PSPath -name $property | select -expand $property
    }
}

if (Test-Path "hklm:\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full") {
    switch (Get-KeyPropertyValue (Get-Item "hklm:\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full") Release) {
        461814 {$message+='.NET Framework : 4.0'|Out-String;break; }
        378389 {$message+='.NET Framework : 4.5'|Out-String;  break; }
        378675 {$message+='.NET Framework : 4.5.1 installed with Windows 8.1 or Windows Server 2012 R2'|Out-String; break }
        378758 {$message+='.NET Framework : 4.5.1 installed on Windows 8, Windows 7 SP1, or Windows Vista SP2'|Out-String ;  break; }
        379893 {$message+='.NET Framework : 4.5.2'|Out-String ; break;}
        528040 {$message+='.NET Framework : 4.8.0 Windows 10 May 2019 Update'|Out-String;$net=4.8;break;}
        528049 {$message+='.NET Framework : 4.8.0 Other than windows OS'|Out-String;   break;}
        461814 {$message+='.NET Framework : 4.7.2 Windows 10 Oct 2018 Update'|Out-String;   break;}
        461808 {$message+='.NET Framework : 4.7.2 Windows 10 April 2018 Update'|Out-String;   break;}
        461814 {$message+='.NET Framework : 4.7.2 All Other OS April 2018 Update'|Out-String;  break;}
        461814 {$message+='.NET Framework : 4.7.2 All Other OS April 2018 Update'|Out-String ;  break;}
        461308 {$message+='.NET Framework : 4.7.1 Windows 10 Creators Update'|Out-String ;  break;}
        461310 {$message+='.NET Framework : 4.7.1 All Others OS Creators Update '|Out-String;   break;}
        460798 {$message+='.NET Framework : 4.7 Windows 10 Creators update'|Out-String;   break;}
        460805 {$message+='.NET Framework : 4.7 All others OS version'|Out-String;   break;}

        { 393295, 393297 -contains $_} {".NET Framework : 4.6" ;-BackgroundColor Green -ForegroundColor Black; break; }
        { 394254, 394271 -contains $_} {".NET Framework : 4.6.1";-BackgroundColor Green -ForegroundColor Black; break; }
        { 394802, 394806 -contains $_} {".NET Framework : 4.6.2";-BackgroundColor Green -ForegroundColor Black; break; }
    }
}

else
{
  $errorrs+='.NetFramework 4.0 is not installed use Link : https://docs.microsoft.com/en-us/dotnet/framework/install/on-windows-10'|out-string
}
  if(($net -gt 4))
    {
    $output='Success' | Out-String;
    #Start-Process -FilePath "C:\Users\v-jahgan\Downloads\npp.7.8.6.Installer.x64"    
    }
    else
    {
    $output='Failed' | Out-String;
    }
	$output | Out-String;
