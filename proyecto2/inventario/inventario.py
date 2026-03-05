# Módulo de inventario para manejo de productos
import json
import csv
import os

class Inventario:
    def __init__(self, archivo_json='inventario/data/productos.json'):
        self.archivo_json = archivo_json
        self.productos = self.cargar_productos()
    
    def cargar_productos(self):
        """Carga los productos desde el archivo JSON"""
        if os.path.exists(self.archivo_json):
            try:
                with open(self.archivo_json, 'r', encoding='utf-8') as file:
                    return json.load(file)
            except (json.JSONDecodeError, FileNotFoundError):
                return []
        return []
    
    def guardar_productos(self):
        """Guarda los productos en el archivo JSON"""
        os.makedirs(os.path.dirname(self.archivo_json), exist_ok=True)
        with open(self.archivo_json, 'w', encoding='utf-8') as file:
            json.dump(self.productos, file, indent=4, ensure_ascii=False)
    
    def agregar_producto(self, producto):
        """Agrega un nuevo producto al inventario"""
        self.productos.append(producto)
        self.guardar_productos()
    
    def eliminar_producto(self, index):
        """Elimina un producto por su índice"""
        if 0 <= index < len(self.productos):
            self.productos.pop(index)
            self.guardar_productos()
            return True
        return False
    
    def actualizar_producto(self, index, producto):
        """Actualiza un producto existente"""
        if 0 <= index < len(self.productos):
            self.productos[index] = producto
            self.guardar_productos()
            return True
        return False