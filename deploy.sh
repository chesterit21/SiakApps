#!/bin/bash

# SIAK Docker Deployment Script
# Usage: ./deploy.sh [command]

set -e

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Functions
print_success() {
    echo -e "${GREEN}✓ $1${NC}"
}

print_error() {
    echo -e "${RED}✗ $1${NC}"
}

print_info() {
    echo -e "${YELLOW}ℹ $1${NC}"
}

# Check if Docker is running
check_docker() {
    if ! docker info > /dev/null 2>&1; then
        print_error "Docker is not running. Please start Docker first."
        exit 1
    fi
    print_success "Docker is running"
}

# Build and start containers
start() {
    print_info "Building and starting containers..."
    docker compose up -d --build
    print_success "Containers started successfully"
    
    print_info "Waiting for services to be healthy..."
    sleep 5
    docker compose ps
    
    print_info "\nAccess the application at: http://localhost:5000"
    print_info "Health check: http://localhost:5000/health"
}

# Stop containers (pause)
stop() {
    print_info "Stopping containers (pausing)..."
    docker compose stop
    print_success "Containers stopped (paused)"
}

# Down containers (remove)
down() {
    print_info "Removing containers..."
    docker compose down
    print_success "Containers removed"
}

# Restart containers
restart() {
    print_info "Restarting containers..."
    docker compose restart
    print_success "Containers restarted"
}

# View logs
logs() {
    if [ -z "$1" ]; then
        docker compose logs -f
    else
        docker compose logs -f "$1"
    fi
}

# Show status
status() {
    print_info "Container Status:"
    docker compose ps
    
    print_info "\nResource Usage:"
    docker stats --no-stream siak_web postgres_db 2>/dev/null || true
    
    print_info "\nHealth Check:"
    curl -s http://localhost:5000/health | jq . 2>/dev/null || curl -s http://localhost:5000/health
}

# Clean up (remove containers and volumes)
clean() {
    read -p "⚠️  This will remove all containers and volumes. Are you sure? (y/N) " -n 1 -r
    echo
    if [[ $REPLY =~ ^[Yy]$ ]]; then
        print_info "Cleaning up..."
        docker compose down -v
        print_success "Cleanup completed"
    else
        print_info "Cleanup cancelled"
    fi
}

# Rebuild specific service
rebuild() {
    if [ -z "$1" ]; then
        print_error "Please specify a service name (siak_web or postgres_db)"
        exit 1
    fi
    
    print_info "Rebuilding $1..."
    docker compose build --no-cache "$1"
    docker compose up -d "$1"
    print_success "$1 rebuilt and restarted"
}

# Backup database
backup() {
    BACKUP_FILE="backup_$(date +%Y%m%d_%H%M%S).sql"
    print_info "Creating database backup: $BACKUP_FILE"
    docker exec postgres_db pg_dump -U postgres sfcore_siak > "$BACKUP_FILE"
    print_success "Backup created: $BACKUP_FILE"
}

# Restore database
restore() {
    if [ -z "$1" ]; then
        print_error "Please specify backup file"
        exit 1
    fi
    
    if [ ! -f "$1" ]; then
        print_error "Backup file not found: $1"
        exit 1
    fi
    
    read -p "⚠️  This will restore database from $1. Continue? (y/N) " -n 1 -r
    echo
    if [[ $REPLY =~ ^[Yy]$ ]]; then
        print_info "Restoring database from $1..."
        docker exec -i postgres_db psql -U postgres sfcore_siak < "$1"
        print_success "Database restored"
    else
        print_info "Restore cancelled"
    fi
}

# Show help
help() {
    cat << EOF
SIAK Docker Deployment Script

Usage: ./deploy.sh [command] [options]

Commands:
    start           Build and start all containers
    stop            Stop containers (pause, can be restarted)
    down            Remove containers (must rebuild to start again)
    restart         Restart all containers
    logs [service]  View logs (optionally for specific service)
    status          Show container status and health
    clean           Remove all containers and volumes (⚠️  destructive)
    rebuild <svc>   Rebuild specific service (siak_web)
    backup          Backup PostgreSQL database
    restore <file>  Restore database from backup file
    help            Show this help message

Examples:
    ./deploy.sh start
    ./deploy.sh logs siak_web
    ./deploy.sh rebuild siak_web
    ./deploy.sh backup
    ./deploy.sh restore backup_20250118_120000.sql

EOF
}

# Main script
check_docker

case "$1" in
    start)
        start
        ;;
    stop)
        stop
        ;;
    down)
        down
        ;;
    restart)
        restart
        ;;
    logs)
        logs "$2"
        ;;
    status)
        status
        ;;
    clean)
        clean
        ;;
    rebuild)
        rebuild "$2"
        ;;
    backup)
        backup
        ;;
    restore)
        restore "$2"
        ;;
    help|--help|-h|"")
        help
        ;;
    *)
        print_error "Unknown command: $1"
        echo ""
        help
        exit 1
        ;;
esac
