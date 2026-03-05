"""
Módulo para manejo de base de datos SQLite con SQLAlchemy
Este archivo contiene funciones auxiliares para interactuar con la base de datos
"""

from sqlalchemy import create_engine, text
from sqlalchemy.orm import sessionmaker
import os
import csv
import json
from datetime import datetime

# Configuración de la base de datos
DATABASE_URL = 'sqlite:///inventario.db'
engine = create_engine(DATABASE_URL, echo=True)
Session = sessionmaker(bind=engine)

def get_session():
    """Obtiene una sesión de base de datos"""
    return Session()

def init_db():
    """Inicializa la base de datos creando las tablas si no existen"""
    from app import db
    db.create_all()
    print("Base de datos inicializada correctamente")

def ejecutar_consulta(consulta, parametros=None):
    """
    Ejecuta una consulta SQL directa
    Útil para consultas personalizadas o reportes
    """
    session = get_session()
    try:
        if parametros:
            resultado = session.execute(text(consulta), parametros)
        else:
            resultado = session.execute(text(consulta))
        session.commit()
        return resultado
    except Exception as e:
        session.rollback()
        print(f"Error en la consulta: {e}")
        return None
    finally:
        session.close()

def backup_datos(formato='json'):
    """
    Realiza backup de los datos de la base de datos a diferentes formatos
    formatos: 'json', 'csv', 'txt'
    """
    from app import Producto
    
    session = get_session()
    try:
        productos = session.query(Producto).all()
        
        # Crear directorio de backups si no existe
        os.makedirs('inventario/backups', exist_ok=True)
        
        timestamp = datetime.now().strftime('%Y%m%d_%H%M%S')
        
        if formato == 'json':
            # Backup a JSON
            archivo = f'inventario/backups/backup_{timestamp}.json'
            datos = [p.to_dict() for p in productos]
            with open(archivo, 'w', encoding='utf-8') as f:
                json.dump(datos, f, indent=4, ensure_ascii=False)
            print(f"Backup JSON creado: {archivo}")
            
        elif formato == 'csv':
            # Backup a CSV
            archivo = f'inventario/backups/backup_{timestamp}.csv'
            with open(archivo, 'w', newline='', encoding='utf-8') as f:
                writer = csv.writer(f)
                writer.writerow(['ID', 'Nombre', 'Precio', 'Cantidad', 'Categoria', 'Fecha_Registro'])
                for p in productos:
                    writer.writerow([
                        p.id, 
                        p.nombre, 
                        p.precio, 
                        p.cantidad, 
                        p.categoria,
                        p.fecha_registro.strftime('%Y-%m-%d %H:%M:%S')
                    ])
            print(f"Backup CSV creado: {archivo}")
            
        elif formato == 'txt':
            # Backup a TXT
            archivo = f'inventario/backups/backup_{timestamp}.txt'
            with open(archivo, 'w', encoding='utf-8') as f:
                f.write("=== BACKUP DE INVENTARIO ===\n")
                f.write(f"Fecha: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}\n")
                f.write("="*50 + "\n\n")
                
                for p in productos:
                    f.write(f"ID: {p.id}\n")
                    f.write(f"Nombre: {p.nombre}\n")
                    f.write(f"Precio: ${p.precio:.2f}\n")
                    f.write(f"Cantidad: {p.cantidad}\n")
                    f.write(f"Categoría: {p.categoria}\n")
                    f.write(f"Fecha Registro: {p.fecha_registro.strftime('%Y-%m-%d %H:%M:%S')}\n")
                    f.write("-"*30 + "\n")
            print(f"Backup TXT creado: {archivo}")
        
        return archivo
    except Exception as e:
        print(f"Error al crear backup: {e}")
        return None
    finally:
        session.close()

def restaurar_backup(archivo_backup):
    """
    Restaura datos desde un archivo de backup
    """
    from app import Producto, db
    
    session = get_session()
    try:
        # Determinar formato por extensión
        extension = archivo_backup.split('.')[-1].lower()
        
        if extension == 'json':
            with open(archivo_backup, 'r', encoding='utf-8') as f:
                datos = json.load(f)
            
            # Limpiar tabla actual
            session.query(Producto).delete()
            
            # Insertar datos del backup
            for d in datos:
                producto = Producto(
                    nombre=d['nombre'],
                    precio=d['precio'],
                    cantidad=d['cantidad'],
                    categoria=d['categoria']
                )
                session.add(producto)
            
            session.commit()
            print(f"Datos restaurados desde {archivo_backup}")
            
        elif extension == 'csv':
            with open(archivo_backup, 'r', encoding='utf-8') as f:
                reader = csv.DictReader(f)
                
                # Limpiar tabla actual
                session.query(Producto).delete()
                
                for row in reader:
                    producto = Producto(
                        nombre=row['Nombre'],
                        precio=float(row['Precio']),
                        cantidad=int(row['Cantidad']),
                        categoria=row['Categoria']
                    )
                    session.add(producto)
                
                session.commit()
                print(f"Datos restaurados desde {archivo_backup}")
        
        return True
    except Exception as e:
        session.rollback()
        print(f"Error al restaurar backup: {e}")
        return False
    finally:
        session.close()

def exportar_a_formato(formato='json'):
    """
    Exporta los datos de la base de datos al formato especificado
    en la carpeta data/
    """
    from app import Producto
    
    session = get_session()
    try:
        productos = session.query(Producto).all()
        os.makedirs('inventario/data', exist_ok=True)
        
        if formato == 'json':
            archivo = 'inventario/data/productos_export.json'
            datos = [p.to_dict() for p in productos]
            with open(archivo, 'w', encoding='utf-8') as f:
                json.dump(datos, f, indent=4, ensure_ascii=False)
            print(f"Datos exportados a JSON: {archivo}")
            
        elif formato == 'csv':
            archivo = 'inventario/data/productos_export.csv'
            with open(archivo, 'w', newline='', encoding='utf-8') as f:
                writer = csv.writer(f)
                writer.writerow(['ID', 'Nombre', 'Precio', 'Cantidad', 'Categoria', 'Fecha_Registro'])
                for p in productos:
                    writer.writerow([
                        p.id,
                        p.nombre,
                        p.precio,
                        p.cantidad,
                        p.categoria,
                        p.fecha_registro.strftime('%Y-%m-%d %H:%M:%S')
                    ])
            print(f"Datos exportados a CSV: {archivo}")
            
        elif formato == 'txt':
            archivo = 'inventario/data/productos_export.txt'
            with open(archivo, 'w', encoding='utf-8') as f:
                f.write("LISTA DE PRODUCTOS\n")
                f.write("="*50 + "\n\n")
                for p in productos:
                    f.write(f"ID: {p.id}\n")
                    f.write(f"Nombre: {p.nombre}\n")
                    f.write(f"Precio: ${p.precio:.2f}\n")
                    f.write(f"Cantidad: {p.cantidad}\n")
                    f.write(f"Categoría: {p.categoria}\n")
                    f.write(f"Fecha: {p.fecha_registro.strftime('%Y-%m-%d %H:%M:%S')}\n")
                    f.write("-"*30 + "\n")
            print(f"Datos exportados a TXT: {archivo}")
        
        return archivo
    except Exception as e:
        print(f"Error al exportar datos: {e}")
        return None
    finally:
        session.close()

def estadisticas_bd():
    """
    Obtiene estadísticas de la base de datos
    """
    from app import Producto
    from sqlalchemy import func
    
    session = get_session()
    try:
        stats = {
            'total_productos': session.query(Producto).count(),
            'precio_promedio': session.query(func.avg(Producto.precio)).scalar() or 0,
            'total_stock': session.query(func.sum(Producto.cantidad)).scalar() or 0,
            'producto_mas_caro': session.query(Producto).order_by(Producto.precio.desc()).first(),
            'producto_mas_barato': session.query(Producto).order_by(Producto.precio.asc()).first(),
            'categorias': {}
        }
        
        # Estadísticas por categoría
        categorias = session.query(
            Producto.categoria, 
            func.count(Producto.id),
            func.avg(Producto.precio),
            func.sum(Producto.cantidad)
        ).group_by(Producto.categoria).all()
        
        for cat in categorias:
            stats['categorias'][cat[0]] = {
                'cantidad': cat[1],
                'precio_promedio': float(cat[2]) if cat[2] else 0,
                'stock_total': int(cat[3]) if cat[3] else 0
            }
        
        return stats
    except Exception as e:
        print(f"Error al obtener estadísticas: {e}")
        return None
    finally:
        session.close()

def buscar_productos(termino):
    """
    Busca productos por nombre o categoría
    """
    from app import Producto
    
    session = get_session()
    try:
        resultados = session.query(Producto).filter(
            (Producto.nombre.contains(termino)) | 
            (Producto.categoria.contains(termino))
        ).all()
        return resultados
    except Exception as e:
        print(f"Error en búsqueda: {e}")
        return []
    finally:
        session.close()

def productos_bajo_stock(limite=10):
    """
    Encuentra productos con stock bajo
    """
    from app import Producto
    
    session = get_session()
    try:
        productos = session.query(Producto).filter(Producto.cantidad <= limite).all()
        return productos
    except Exception as e:
        print(f"Error al buscar productos con bajo stock: {e}")
        return []
    finally:
        session.close()

# Función para inicializar datos de ejemplo
def inicializar_datos_ejemplo():
    """
    Agrega datos de ejemplo a la base de datos si está vacía
    """
    from app import Producto, db
    
    session = get_session()
    try:
        # Verificar si ya hay datos
        if session.query(Producto).count() == 0:
            productos_ejemplo = [
                Producto(nombre="Laptop HP", precio=899.99, cantidad=15, categoria="Electrónica"),
                Producto(nombre="Mouse Inalámbrico", precio=25.50, cantidad=50, categoria="Electrónica"),
                Producto(nombre="Teclado Mecánico", precio=89.99, cantidad=30, categoria="Electrónica"),
                Producto(nombre="Monitor 24 pulgadas", precio=199.99, cantidad=20, categoria="Electrónica"),
                Producto(nombre="Camiseta Algodón", precio=19.99, cantidad=100, categoria="Ropa"),
                Producto(nombre="Pantalón Jeans", precio=49.99, cantidad=45, categoria="Ropa"),
                Producto(nombre="Zapatos Deportivos", precio=79.99, cantidad=25, categoria="Ropa"),
                Producto(nombre="Arroz 5kg", precio=8.99, cantidad=200, categoria="Alimentos"),
                Producto(nombre="Frijoles 1kg", precio=2.99, cantidad=150, categoria="Alimentos"),
                Producto(nombre="Aceite Vegetal", precio=5.99, cantidad=80, categoria="Alimentos"),
                Producto(nombre="Libro Python", precio=45.99, cantidad=35, categoria="Libros"),
                Producto(nombre="Libro JavaScript", precio=39.99, cantidad=40, categoria="Libros"),
            ]
            
            for producto in productos_ejemplo:
                session.add(producto)
            
            session.commit()
            print("Datos de ejemplo inicializados correctamente")
        else:
            print("La base de datos ya contiene datos")
            
    except Exception as e:
        session.rollback()
        print(f"Error al inicializar datos de ejemplo: {e}")
    finally:
        session.close()

if __name__ == "__main__":
    # Si se ejecuta directamente, inicializar datos de ejemplo
    inicializar_datos_ejemplo()
    print("\nEstadísticas de la base de datos:")
    stats = estadisticas_bd()
    if stats:
        print(f"Total de productos: {stats['total_productos']}")
        print(f"Precio promedio: ${stats['precio_promedio']:.2f}")
        print(f"Stock total: {stats['total_stock']}")
        print("\nProductos con bajo stock:")
        bajos = productos_bajo_stock(20)
        for p in bajos:
            print(f"  - {p.nombre}: {p.cantidad} unidades")