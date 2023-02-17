echo off
cd bin/Debug/net6.0
katas.pokedex.migrations.exe /cnn:"Server=.;Database=pokedex;Trusted_Connection=True;"
cd../../..
echo on