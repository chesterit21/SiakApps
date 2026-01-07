```sql
-- =====================================================================
-- Schema untuk table menuApp
-- Menyimpan daftar menu aplikasi dengan struktur hierarki (parent-child)
-- =====================================================================
CREATE TABLE menuApp (
    -- ID unik untuk setiap menu, menggunakan SERIAL untuk auto-increment
    id SERIAL PRIMARY KEY,

    -- Nama menu yang akan ditampilkan di UI
    menuname VARCHAR(100) NOT NULL,

    -- Kode unik untuk referensi di dalam kode, harus unik
    unique_code VARCHAR(50) NOT NULL UNIQUE,

    -- URL atau path untuk navigasi menu
    menu_url VARCHAR(255),

    -- Foreign key ke tabel ini sendiri untuk menunjukkan menu parent.
    -- Nilainya NULL jika menu ini adalah root.
    parent_menu_id INT,

    -- Flag untuk menandakan apakah menu ini adalah parent (memiliki submenu)
    is_parent BOOLEAN NOT NULL DEFAULT FALSE,

    -- Level kedalaman menu dalam struktur hierarki
    level_parent INT NOT NULL DEFAULT 0,

    -- Definisi constraint untuk foreign key parent_menu_id
    CONSTRAINT fk_parent_menu
        FOREIGN KEY(parent_menu_id)
        REFERENCES menuApp(id)
        ON DELETE SET NULL -- Jika parent dihapus, jadikan submenu sebagai root
);

-- Membuat index untuk mempercepat pencarian berdasarkan menuname dan unique_code
CREATE INDEX idx_menuapp_menuname ON menuApp(menuname);
CREATE INDEX idx_menuapp_unique_code ON menuApp(unique_code);

COMMENT ON TABLE menuApp IS 'Tabel untuk menyimpan data menu aplikasi dan strukturnya.';
COMMENT ON COLUMN menuApp.parent_menu_id IS 'Referensi ke ID menu parent dalam tabel yang sama.';


-- =====================================================================
-- Schema untuk table role_menu
-- Tabel mapping antara Roles dan Menu, untuk mengatur hak akses (permissions)
-- =====================================================================
CREATE TABLE role_menu (
    -- ID unik untuk setiap baris permission
    id SERIAL PRIMARY KEY,

    -- Foreign key ke tabel 'roles' (diasumsikan sudah ada)
    role_id INT NOT NULL,

    -- Foreign key ke tabel 'menuApp'
    menu_id INT NOT NULL,

    -- Kumpulan hak akses dalam bentuk boolean
    isView BOOLEAN NOT NULL DEFAULT FALSE,
    isAdd BOOLEAN NOT NULL DEFAULT FALSE,
    isEdit BOOLEAN NOT NULL DEFAULT FALSE,
    isDelete BOOLEAN NOT NULL DEFAULT FALSE,
    isUpload BOOLEAN NOT NULL DEFAULT FALSE,
    isApprove BOOLEAN NOT NULL DEFAULT FALSE,
    isDownload BOOLEAN NOT NULL DEFAULT FALSE,
    isPrint BOOLEAN NOT NULL DEFAULT FALSE,

    -- Definisi constraint untuk foreign key ke tabel roles
    -- Diasumsikan nama tabelnya 'roles' dan primary key-nya 'id'
    CONSTRAINT fk_role
        FOREIGN KEY(role_id)
        REFERENCES roles(id)
        ON DELETE CASCADE, -- Jika role dihapus, permission terkait juga terhapus

    -- Definisi constraint untuk foreign key ke tabel menuApp
    CONSTRAINT fk_menu
        FOREIGN KEY(menu_id)
        REFERENCES menuApp(id)
        ON DELETE CASCADE, -- Jika menu dihapus, permission terkait juga terhapus

    -- Mencegah duplikasi permission untuk role dan menu yang sama
    UNIQUE (role_id, menu_id)
);

COMMENT ON TABLE role_menu IS 'Tabel untuk mengatur hak akses setiap role terhadap menu.';
COMMENT ON COLUMN role_menu.isView IS 'Permission untuk melihat halaman/data.';
COMMENT ON COLUMN role_menu.isAdd IS 'Permission untuk menambah data baru.';

```
