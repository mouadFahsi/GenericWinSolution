param([string]$m, [string]$brench = "master")

if (!$m) { 
Write-Host "Parameter : m is null" 
exit
}

echo "-"
write-host "(- syn GenericWinForm-)" -foreground "magenta"
echo "-"

git status

git pull
git status

git add .
git commit -m "$m"
git push