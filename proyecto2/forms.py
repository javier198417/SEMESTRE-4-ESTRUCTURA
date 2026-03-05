"""
Módulo de formularios para la aplicación Flask
Utiliza Flask-WTF y WTForms para manejo de formularios
"""

from flask_wtf import FlaskForm
from wtforms import StringField, EmailField, TextAreaField, FloatField, IntegerField, SelectField, SubmitField
from wtforms.validators import DataRequired, Email, Length, NumberRange, Optional
from datetime import datetime

class ContactoForm(FlaskForm):
    """
    Formulario para contacto/guardar en archivos
    """
    nombre = StringField(
        'Nombre',
        validators=[
            DataRequired(message='El nombre es obligatorio'),
            Length(min=2, max=100, message='El nombre debe tener entre 2 y 100 caracteres')
        ],
        render_kw={"placeholder": "Ingresa tu nombre completo"}
    )
    
    email = EmailField(
        'Email',
        validators=[
            DataRequired(message='El email es obligatorio'),
            Email(message='Ingresa un email válido')
        ],
        render_kw={"placeholder": "correo@ejemplo.com"}
    )
    
    mensaje = TextAreaField(
        'Mensaje',
        validators=[
            DataRequired(message='El mensaje es obligatorio'),
            Length(min=5, max=500, message='El mensaje debe tener entre 5 y 500 caracteres')
        ],
        render_kw={"placeholder": "Escribe tu mensaje aquí...", "rows": 5}
    )
    
    submit_txt = SubmitField('Guardar en TXT')
    submit_json = SubmitField('Guardar en JSON')
    submit_csv = SubmitField('Guardar en CSV')

class ProductoForm(FlaskForm):
    """
    Formulario para productos (SQLite)
    """
    nombre = StringField(
        'Nombre del Producto',
        validators=[
            DataRequired(message='El nombre del producto es obligatorio'),
            Length(min=2, max=100, message='El nombre debe tener entre 2 y 100 caracteres')
        ],
        render_kw={"placeholder": "Ej: Laptop HP, Mouse Inalámbrico, etc."}
    )
    
    precio = FloatField(
        'Precio',
        validators=[
            DataRequired(message='El precio es obligatorio'),
            NumberRange(min=0.01, max=999999.99, message='El precio debe ser mayor a 0')
        ],
        render_kw={"placeholder": "0.00", "step": "0.01"}
    )
    
    cantidad = IntegerField(
        'Cantidad',
        validators=[
            DataRequired(message='La cantidad es obligatoria'),
            NumberRange(min=0, max=999999, message='La cantidad debe ser un número positivo')
        ],
        render_kw={"placeholder": "0", "min": "0"}
    )
    
    categoria = SelectField(
        'Categoría',
        choices=[
            ('', 'Seleccionar categoría'),
            ('Electrónica', 'Electrónica'),
            ('Ropa', 'Ropa'),
            ('Alimentos', 'Alimentos'),
            ('Libros', 'Libros'),
            ('Hogar', 'Hogar'),
            ('Deportes', 'Deportes'),
            ('Juguetes', 'Juguetes'),
            ('Otros', 'Otros')
        ],
        validators=[DataRequired(message='Selecciona una categoría')]
    )
    
    submit = SubmitField('Guardar Producto')

class BusquedaForm(FlaskForm):
    """
    Formulario para búsqueda de productos
    """
    termino = StringField(
        'Buscar producto',
        validators=[Optional()],
        render_kw={"placeholder": "Buscar por nombre o categoría..."}
    )
    
    submit = SubmitField('Buscar')

class ReporteForm(FlaskForm):
    """
    Formulario para generar reportes
    """
    formato = SelectField(
        'Formato de reporte',
        choices=[
            ('txt', 'Archivo TXT'),
            ('json', 'Archivo JSON'),
            ('csv', 'Archivo CSV')
        ],
        validators=[DataRequired(message='Selecciona un formato')]
    )
    
    fecha_inicio = StringField(
        'Fecha inicio (opcional)',
        validators=[Optional()],
        render_kw={"placeholder": "YYYY-MM-DD", "type": "date"}
    )
    
    fecha_fin = StringField(
        'Fecha fin (opcional)',
        validators=[Optional()],
        render_kw={"placeholder": "YYYY-MM-DD", "type": "date"}
    )
    
    submit = SubmitField('Generar Reporte')

class BackupForm(FlaskForm):
    """
    Formulario para backup/restauración
    """
    formato = SelectField(
        'Formato de backup',
        choices=[
            ('json', 'JSON'),
            ('csv', 'CSV'),
            ('txt', 'TXT')
        ],
        validators=[DataRequired(message='Selecciona un formato')]
    )
    
    submit_backup = SubmitField('Crear Backup')
    submit_restore = SubmitField('Restaurar Backup')

class FiltroProductosForm(FlaskForm):
    """
    Formulario para filtrar productos
    """
    categoria = SelectField(
        'Filtrar por categoría',
        choices=[
            ('', 'Todas las categorías'),
            ('Electrónica', 'Electrónica'),
            ('Ropa', 'Ropa'),
            ('Alimentos', 'Alimentos'),
            ('Libros', 'Libros'),
            ('Hogar', 'Hogar'),
            ('Deportes', 'Deportes'),
            ('Juguetes', 'Juguetes'),
            ('Otros', 'Otros')
        ],
        validators=[Optional()]
    )
    
    precio_min = FloatField(
        'Precio mínimo',
        validators=[Optional(), NumberRange(min=0)],
        render_kw={"placeholder": "Mínimo", "step": "0.01"}
    )
    
    precio_max = FloatField(
        'Precio máximo',
        validators=[Optional(), NumberRange(min=0)],
        render_kw={"placeholder": "Máximo", "step": "0.01"}
    )
    
    stock_min = IntegerField(
        'Stock mínimo',
        validators=[Optional(), NumberRange(min=0)],
        render_kw={"placeholder": "Stock mínimo"}
    )
    
    submit = SubmitField('Aplicar Filtros')

class ProductoEliminarForm(FlaskForm):
    """
    Formulario para confirmar eliminación de producto
    """
    confirmacion = StringField(
        'Escribe "ELIMINAR" para confirmar',
        validators=[
            DataRequired(message='Debes confirmar la eliminación'),
            Length(min=7, max=7, message='Debes escribir exactamente "ELIMINAR"')
        ],
        render_kw={"placeholder": "ELIMINAR"}
    )
    
    submit = SubmitField('Confirmar Eliminación')

# NOTA: Los ejemplos de uso deben ir en app.py, NO aquí
# Los comentarios de ejemplo se han eliminado para evitar errores de Pylance