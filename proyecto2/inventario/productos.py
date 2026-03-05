# Módulo para manejo de productos
class Producto:
    def __init__(self, nombre, precio, cantidad, categoria):
        self.nombre = nombre
        self.precio = precio
        self.cantidad = cantidad
        self.categoria = categoria
    
    def to_dict(self):
        """Convierte el objeto a diccionario"""
        return {
            'nombre': self.nombre,
            'precio': self.precio,
            'cantidad': self.cantidad,
            'categoria': self.categoria
        }
    
    @staticmethod
    def from_dict(data):
        """Crea un objeto Producto desde un diccionario"""
       