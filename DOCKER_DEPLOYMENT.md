# 🐳 SIAK Web Apps - Docker Deployment Guide

Panduan lengkap untuk deploy aplikasi SIAK menggunakan Docker dan Docker Compose.

## 📋 Prerequisites

- Docker Engine 20.10+
- Docker Compose 2.0+
- Port 5000 (web app) dan 5432 (PostgreSQL) harus tersedia

## 🏗️ Architecture

```
┌─────────────────────────────────────────┐
│      siak_network (custom bridge)       │
│                                         │
│  ┌──────────────┐   ┌──────────────┐  │
│  │  postgres_db │   │   siak_web   │  │
│  │  (existing)  │   │  (deployed)  │  │
│  │ PostgreSQL   │◄──┤  ASP.NET 10  │  │
│  │   :5432      │   │    :8080     │  │
│  └──────────────┘   └──────────────┘  │
│         │                   │          │
└─────────┼───────────────────┼──────────┘
          │                   │
     Port 5432           Port 5000
  (host access)      (host access)
```

**Note**: Container `postgres_db` sudah ada sebelumnya. Docker Compose hanya deploy `siak_web` dan connect ke network yang sama.

## 📁 File Structure

```
SiakApps/
├── Dockerfile                          # Multi-stage build untuk .NET 10
├── docker-compose.yml                  # Orchestration file (siak_web only)
├── .dockerignore                       # Exclude files dari build
├── deploy.sh                           # Helper script untuk deployment
└── SiakWebApps/
    ├── appsettings.json               # Development config
    └── appsettings.Production.json    # Production config (Docker)
```

## 🚀 Quick Start

### Prerequisites

Pastikan network `siak_network` sudah dibuat dan `postgres_db` sudah connected:

```bash
# Create network (jika belum ada)
docker network create siak_network

# Connect postgres_db ke network (jika belum)
docker network connect siak_network postgres_db
```

### 1. Build dan Start Containers

**Option 1: Using Helper Script (Recommended)**

```bash
cd /home/sfcore/SFCoreApps/DummyApps/SiakApps

# Build dan start
./deploy.sh start
```

**Option 2: Using Docker Compose Directly**

```bash
# Build dan start
docker compose up -d --build
```

### 2. Verify Deployment

```bash
# Using helper script
./deploy.sh status

# Or manually
docker ps --filter "name=siak_web"
curl http://localhost:5000/health
```

### 3. Access Application

- **Web App**: http://localhost:5000
- **Health Check**: http://localhost:5000/health
- **PostgreSQL**: localhost:5432 (existing container)
  - Database: `sfcore_siak`
  - Username: `postgres`
  - Password: `PasswordGua_123!`

## �️ Helper Script Commands (deploy.sh)

The `deploy.sh` script provides convenient commands for managing your deployment:

### Deployment Commands

```bash
# Start containers (build if needed)
./deploy.sh start

# Stop containers (pause, can restart quickly)
./deploy.sh stop

# Remove containers (need rebuild to start)
./deploy.sh down

# Restart containers
./deploy.sh restart
```

### Monitoring Commands

```bash
# View all logs
./deploy.sh logs

# View specific service logs
./deploy.sh logs siak_web

# Show container status and health
./deploy.sh status
```

### Maintenance Commands

```bash
# Rebuild specific service
./deploy.sh rebuild siak_web

# Backup database
./deploy.sh backup

# Restore database from backup
./deploy.sh restore backup_20250118_120000.sql

# Clean up (⚠️ removes containers and volumes)
./deploy.sh clean
```

### Command Comparison

| Command | Container Status | Image | Volume | Restart Speed |
|---------|-----------------|-------|--------|---------------|
| `stop` | Stopped (paused) | ✅ Kept | ✅ Kept | ⚡ Fast |
| `down` | Removed | ✅ Kept | ✅ Kept | 🔨 Need rebuild |
| `clean` | Removed | ✅ Kept | ❌ **DELETED** | 🔨 Need rebuild |

## �🔧 Docker Compose Commands

If you prefer using Docker Compose directly:

### Start/Stop Services

```bash
# Start services
docker compose up -d

# Stop services (pause)
docker compose stop

# Remove containers
docker compose down

# Stop dan remove volumes (WARNING: Data akan hilang!)
docker compose down -v
```

### View Logs

```bash
# Using helper script
./deploy.sh logs siak_web

# Or using docker compose
docker compose logs -f siak_web

# Last 100 lines
docker compose logs --tail=100 siak_web
```

### Rebuild Application

```bash
# Using helper script (recommended)
./deploy.sh rebuild siak_web

# Or using docker compose
docker compose build --no-cache siak_web
docker compose up -d siak_web
```

### Execute Commands in Container

```bash
# Access web app container shell
docker exec -it siak_web bash

# Access PostgreSQL
docker exec -it postgres_db psql -U postgres -d sfcore_siak

# Run migrations or scripts
docker exec -it siak_web dotnet ef database update
```

## 🔍 Troubleshooting

### Container tidak start

```bash
# Check logs untuk error messages
./deploy.sh logs siak_web

# Or manually
docker compose logs siak_web

# Check network
docker network ls
docker network inspect siak_network
```

### Database connection failed

```bash
# Verify postgres is running
docker ps --filter "name=postgres_db"

# Check connection string
docker exec -it siak_web env | grep ConnectionStrings

# Test connection dari web container
docker exec -it siak_web bash
apt-get update && apt-get install -y postgresql-client
psql -h postgres_db -U postgres -d sfcore_siak
```

### Port already in use

```bash
# Check what's using the port
sudo lsof -i :5000
sudo lsof -i :5432

# Change port di docker-compose.yml
# Ubah "5000:8080" menjadi "5001:8080" misalnya
```

## 🔐 Security Notes

⚠️ **IMPORTANT**: File `docker-compose.yml` dan `appsettings.Production.json` berisi password dalam plaintext. Untuk production:

1. **Gunakan Docker Secrets**:
```yaml
secrets:
  db_password:
    file: ./secrets/db_password.txt
```

2. **Gunakan Environment Variables**:
```bash
export POSTGRES_PASSWORD="your-secure-password"
docker-compose up -d
```

3. **Gunakan .env file**:
```bash
# .env
POSTGRES_PASSWORD=your-secure-password
```

## 📊 Monitoring

### Health Checks

- **Web App**: http://localhost:5000/health
- **PostgreSQL**: Automatic health check setiap 10 detik

### Resource Usage

```bash
# Monitor resource usage
docker stats siak_web postgres_db

# Disk usage
docker system df
```

## 🔄 Updates & Maintenance

### Update Application Code

```bash
# Pull latest code
git pull

# Rebuild dan restart using helper script
./deploy.sh rebuild siak_web

# Or using docker compose
docker compose up -d --build siak_web
```

### Backup Database

```bash
# Using helper script
./deploy.sh backup

# Or manually
docker exec postgres_db pg_dump -U postgres sfcore_siak > backup_$(date +%Y%m%d).sql
```

### Restore Database

```bash
# Using helper script
./deploy.sh restore backup_20250118.sql

# Or manually
docker exec -i postgres_db psql -U postgres sfcore_siak < backup_20250118.sql
```

### Clean Up

```bash
# Remove siak_web container
./deploy.sh down

# Remove unused images
docker image prune -a

# Remove unused volumes (WARNING!)
docker volume prune
```

## 🌐 Network Details

- **Network Name**: `siak_network`
- **Driver**: bridge
- **Type**: External (created manually)
- **Services**:
  - `postgres_db`: PostgreSQL database (existing container)
  - `siak_web`: ASP.NET Core web application (deployed via docker-compose)

## 📦 Volumes

- **postgres_data**: Managed by existing `postgres_db` container
  - Location: Docker managed volume
  - Backup recommended!
  - Not managed by docker-compose.yml

## 🎯 Production Checklist

- [ ] Change default passwords
- [ ] Use environment variables for secrets
- [ ] Enable HTTPS/SSL
- [ ] Configure reverse proxy (nginx/traefik)
- [ ] Set up automated backups
- [ ] Configure log aggregation
- [ ] Set resource limits
- [ ] Enable monitoring/alerting

## 📞 Support

Jika ada masalah, check:
1. Container logs: `./deploy.sh logs` atau `docker compose logs`
2. Network connectivity: `docker network inspect siak_network`
3. Health status: `./deploy.sh status` atau `docker ps`

## 🎓 Typical Workflow

```bash
# 1. Initial deployment
./deploy.sh start

# 2. Monitor application
./deploy.sh status
./deploy.sh logs siak_web

# 3. Update code
git pull
./deploy.sh rebuild siak_web

# 4. Backup database
./deploy.sh backup

# 5. Stop temporarily (quick restart)
./deploy.sh stop
./deploy.sh start

# 6. Remove container (need rebuild)
./deploy.sh down
./deploy.sh start
```

---

**Created**: 2025-12-18  
**Updated**: 2025-12-18  
**Version**: 1.1.0  
**.NET Version**: 10.0  
**PostgreSQL Version**: 16 (existing container)
