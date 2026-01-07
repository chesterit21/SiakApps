#!/bin/bash

# Script untuk menyelesaikan setup library lokal
# Memastikan semua file telah diunduh dengan benar dan struktur folder sesuai

echo "Memeriksa struktur folder dan file..."

# Pindah ke direktori proyek
cd /home/sfcore/SFCoreApps/DummyApps/SiakApps

# Membuat direktori jika belum ada
mkdir -p SiakWebApps/wwwroot/libs/datatables/css
mkdir -p SiakWebApps/wwwroot/libs/datatables/js
mkdir -p SiakWebApps/wwwroot/libs/fontawesome/css
mkdir -p SiakWebApps/wwwroot/libs/fontawesome/webfonts

# Download file-file jika belum ada
if [ ! -f "SiakWebApps/wwwroot/libs/datatables/css/dataTables.bootstrap5.min.css" ]; then
    echo "Mengunduh DataTables CSS..."
    wget -O SiakWebApps/wwwroot/libs/datatables/css/dataTables.bootstrap5.min.css \
      https://cdn.datatables.net/2.3.5/css/dataTables.bootstrap5.min.css
fi

if [ ! -f "SiakWebApps/wwwroot/libs/datatables/js/dataTables.min.js" ]; then
    echo "Mengunduh DataTables JS..."
    wget -O SiakWebApps/wwwroot/libs/datatables/js/dataTables.min.js \
      https://cdn.datatables.net/2.3.5/js/dataTables.min.js
fi

if [ ! -f "SiakWebApps/wwwroot/libs/datatables/js/dataTables.bootstrap5.min.js" ]; then
    echo "Mengunduh DataTables Bootstrap JS..."
    wget -O SiakWebApps/wwwroot/libs/datatables/js/dataTables.bootstrap5.min.js \
      https://cdn.datatables.net/2.3.5/js/dataTables.bootstrap5.min.js
fi

if [ ! -f "SiakWebApps/wwwroot/libs/fontawesome/css/all.min.css" ]; then
    echo "Mengunduh Font Awesome CSS..."
    wget -O SiakWebApps/wwwroot/libs/fontawesome/css/all.min.css \
      https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css
fi

if [ ! -f "SiakWebApps/wwwroot/libs/fontawesome/webfonts/fa-brands-400.woff2" ]; then
    echo "Mengunduh Font Awesome Brands..."
    wget -O SiakWebApps/wwwroot/libs/fontawesome/webfonts/fa-brands-400.woff2 \
      https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/webfonts/fa-brands-400.woff2
fi

if [ ! -f "SiakWebApps/wwwroot/libs/fontawesome/webfonts/fa-regular-400.woff2" ]; then
    echo "Mengunduh Font Awesome Regular..."
    wget -O SiakWebApps/wwwroot/libs/fontawesome/webfonts/fa-regular-400.woff2 \
      https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/webfonts/fa-regular-400.woff2
fi

if [ ! -f "SiakWebApps/wwwroot/libs/fontawesome/webfonts/fa-solid-900.woff2" ]; then
    echo "Mengunduh Font Awesome Solid..."
    wget -O SiakWebApps/wwwroot/libs/fontawesome/webfonts/fa-solid-900.woff2 \
      https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/webfonts/fa-solid-900.woff2
fi

echo ""
echo "File-file telah diunduh ke folder lokal:"
echo "- DataTables CSS: SiakWebApps/wwwroot/libs/datatables/css/"
echo "- DataTables JS: SiakWebApps/wwwroot/libs/datatables/js/"
echo "- Font Awesome CSS: SiakWebApps/wwwroot/libs/fontawesome/css/"
echo "- Font Awesome Webfonts: SiakWebApps/wwwroot/libs/fontawesome/webfonts/"
echo ""
echo "File _Layout.cshtml telah diperbarui untuk menggunakan file-file lokal."
echo ""
echo "Proses selesai! Aplikasi sekarang akan menggunakan library lokal daripada CDN."