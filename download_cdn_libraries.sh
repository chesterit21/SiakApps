#!/bin/bash

# Script untuk mengunduh semua library CDN ke folder lokal wwwroot/libs
# agar aplikasi tidak tergantung pada CDN eksternal

set -e  # Keluar jika ada error

echo "Memulai pengunduhan library CDN ke folder lokal..."

# Buat direktori wwwroot/libs dan subfolder yang dibutuhkan
mkdir -p wwwroot/libs/datatables/css
mkdir -p wwwroot/libs/datatables/js
mkdir -p wwwroot/libs/fontawesome/css
mkdir -p wwwroot/libs/fontawesome/webfonts

echo "Mengunduh DataTables..."

# DataTables CSS
wget -O wwwroot/libs/datatables/css/dataTables.bootstrap5.min.css \
  https://cdn.datatables.net/2.3.5/css/dataTables.bootstrap5.min.css

# DataTables JS
wget -O wwwroot/libs/datatables/js/dataTables.min.js \
  https://cdn.datatables.net/2.3.5/js/dataTables.min.js

wget -O wwwroot/libs/datatables/js/dataTables.bootstrap5.min.js \
  https://cdn.datatables.net/2.3.5/js/dataTables.bootstrap5.min.js

echo "Mengunduh DataTables Buttons & Dependencies (Excel, PDF)..."

# JSZip (Excel) & PDFMake (PDF)
wget -O wwwroot/libs/datatables/js/jszip.min.js \
  https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js
wget -O wwwroot/libs/datatables/js/pdfmake.min.js \
  https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.2.7/pdfmake.min.js
wget -O wwwroot/libs/datatables/js/vfs_fonts.js \
  https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.2.7/vfs_fonts.js

# Buttons Core & Extensions
wget -O wwwroot/libs/datatables/js/dataTables.buttons.min.js \
  https://cdn.datatables.net/buttons/2.4.2/js/dataTables.buttons.min.js
wget -O wwwroot/libs/datatables/js/buttons.html5.min.js \
  https://cdn.datatables.net/buttons/2.4.2/js/buttons.html5.min.js
wget -O wwwroot/libs/datatables/js/buttons.print.min.js \
  https://cdn.datatables.net/buttons/2.4.2/js/buttons.print.min.js
wget -O wwwroot/libs/datatables/js/buttons.bootstrap5.min.js \
  https://cdn.datatables.net/buttons/2.4.2/js/buttons.bootstrap5.min.js
wget -O wwwroot/libs/datatables/css/buttons.bootstrap5.min.css \
  https://cdn.datatables.net/buttons/2.4.2/css/buttons.bootstrap5.min.css

echo "Mengunduh Font Awesome..."

# Font Awesome CSS
wget -O wwwroot/libs/fontawesome/css/all.min.css \
  https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css

# Font Awesome fonts (jika tersedia)
# Font Awesome 5.15.4 menggunakan folder webfonts
wget -O wwwroot/libs/fontawesome/webfonts/fa-brands-400.woff2 \
  https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/webfonts/fa-brands-400.woff2

wget -O wwwroot/libs/fontawesome/webfonts/fa-regular-400.woff2 \
  https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/webfonts/fa-regular-400.woff2

wget -O wwwroot/libs/fontawesome/webfonts/fa-solid-900.woff2 \
  https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/webfonts/fa-solid-900.woff2

echo "Pengunduhan selesai!"

echo ""
echo "Perubahan yang perlu dilakukan di _Layout.cshtml:"
echo ""
echo "1. Ganti:"
echo "  <link rel=\"stylesheet\" href=\"https://cdn.datatables.net/2.3.5/css/dataTables.bootstrap5.min.css\" />"
echo "  menjadi:"
echo "  <link rel=\"stylesheet\" href=\"~/libs/datatables/css/dataTables.bootstrap5.min.css\" />"
echo ""
echo "2. Ganti:"
echo "  <link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css\" />"
echo "  menjadi:"
echo "  <link rel=\"stylesheet\" href=\"~/libs/fontawesome/css/all.min.css\" />"
echo ""
echo "  Dan tambahkan CSS Buttons:"
echo "  <link rel=\"stylesheet\" href=\"~/libs/datatables/css/buttons.bootstrap5.min.css\" />"
echo ""
echo "3. Ganti:"
echo "  <script src=\"https://cdn.datatables.net/2.3.5/js/dataTables.min.js\"></script>"
echo "  menjadi:"
echo "  <script src=\"~/libs/datatables/js/dataTables.min.js\"></script>"
echo ""
echo "4. Ganti:"
echo "  <script src=\"https://cdn.datatables.net/2.3.5/js/dataTables.bootstrap5.min.js\"></script>"
echo "  menjadi:"
echo "  <script src=\"~/libs/datatables/js/dataTables.bootstrap5.min.js\"></script>"
echo ""
echo "  Dan tambahkan JS Buttons (urutan penting):"
echo "  <script src=\"~/libs/datatables/js/dataTables.buttons.min.js\"></script>"
echo "  <script src=\"~/libs/datatables/js/jszip.min.js\"></script>"
echo "  <script src=\"~/libs/datatables/js/pdfmake.min.js\"></script>"
echo "  <script src=\"~/libs/datatables/js/vfs_fonts.js\"></script>"
echo "  <script src=\"~/libs/datatables/js/buttons.html5.min.js\"></script>"
echo "  <script src=\"~/libs/datatables/js/buttons.print.min.js\"></script>"
echo "  <script src=\"~/libs/datatables/js/buttons.bootstrap5.min.js\"></script>"
echo ""

echo "Proses selesai! Semua library CDN telah diunduh ke folder lokal."