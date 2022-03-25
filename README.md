# Pengaplikasian Algoritma BFS dan DFS dalam Implementasi Folder Crawling


## Table of Contents
* [General Information](#general-information)
* [Requirement and Installation](#requirement-and-installation)
* [How to Run](#how-to-run)
* [Authors](#authors)

## General Information
Program Folder Crawling ini dibuat untuk menyelesaikan Tugas Besar II Mata Kuliah IF2211 Strategi Algoritma Semester II Tahun 2021/2022. Program ini merupakan program sederhana untuk mencari file dalam suatu folder menggunakan metode _graph traversal_ dengan algoritma Breadth First Search (BFS) dan Depth First Search (DFS).

## Requirement and Installation

  - MsBuild
  - Nuget
  - Instalasi Package yang digunakan dengan mengetikkan perintah pada root repository ini
```
nuget install src/packages.config -OutputDirectory packages
```
## How to Run

### Compile


Ketikkan perintah ini pada terminal untuk meng-_compile_ program
```
msbuild src/TubesStima2.csproj /p:OutputPath=../bin /p:IntermediateOutputPath=../bin/obj/ /p:Configuration=Release
```

### Run
1. Buka file TubesStima.exe pada folder bin
2. Pilih folder yang ingin dijadikan sebagai root directory
3. Ketikkan file yang ingin dicari beserta extensionnya
4. Pilih metode yang ingin digunakan (BFS/DFS)
5. Opsional : Centang kasus all occurrences
6. Tekan tombol Search.


## Authors
<table>

<tr><td colspan = 3 align = "center"></td></tr>
    
<tr><td>No.</td><td>Nama</td><td>NIM</td></tr>
<tr><td>1.</td><td><a href="https://github.com/SurTan02"><b>Suryanto</b></a></td><td>13520059</td></tr>
<tr><td>2.</td><td><a href="https://github.com/weslygio"><b>Wesly Giovano</b></a></td><td>13520092</td></tr>
<tr><td>3.</td><td><a href="https://github.com/dawetmaster"><b>Andika Naufal Hilmy</td><td>13520113</td></tr>

</table>
