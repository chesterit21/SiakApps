#!/bin/bash

# Script untuk memperbarui _Layout.cshtml agar menggunakan file-file lokal
# alih-alih CDN eksternal

LAYOUT_FILE="SiakWebApps/Views/Shared/_Layout.cshtml"

if [ ! -f "$LAYOUT_FILE" ]; then
    echo "Error: File $LAYOUT_FILE tidak ditemukan!"
    exit 1
fi

echo "Membuat backup dari _Layout.cshtml..."
cp "$LAYOUT_FILE" "$LAYOUT_FILE.backup"

echo "Memperbarui _Layout.cshtml untuk menggunakan file-file lokal..."

# Ganti link DataTables CSS
sed -i 's|https://cdn.datatables.net/2.3.5/css/dataTables.bootstrap5.min.css|~/libs/datatables/css/dataTables.bootstrap5.min.css|g' "$LAYOUT_FILE"

# Ganti link Font Awesome CSS
sed -i 's|https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css|~/libs/fontawesome/css/all.min.css|g' "$LAYOUT_FILE"

# Ganti link DataTables JS utama
sed -i 's|https://cdn.datatables.net/2.3.5/js/dataTables.min.js|~/libs/datatables/js/dataTables.min.js|g' "$LAYOUT_FILE"

# Ganti link DataTables Bootstrap JS
sed -i 's|https://cdn.datatables.net/2.3.5/js/dataTables.bootstrap5.min.js|~/libs/datatables/js/dataTables.bootstrap5.min.js|g' "$LAYOUT_FILE"

echo "File _Layout.cshtml telah diperbarui untuk menggunakan file-file lokal!"
echo "Backup dari file asli disimpan sebagai _Layout.cshtml.backup"