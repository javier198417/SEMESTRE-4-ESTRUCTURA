from flask import Flask, render_template, request, redirect, url_for, flash
from flask_sqlalchemy import SQLAlchemy
from datetime import datetime
import os
import json
import csv
from forms import ContactoForm, ProductoForm  # Asegúrate de importar los formularios

# Inicializar Flask
app = Flask(__name__)
app.config['SECRET_KEY'] = 'tu-clave-secreta-aqui-cambiar-en-produccion'

# Configuración de SQLAlchemy
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///inventario.db'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False

# Inicializar SQLAlchemy
db = SQLAlchemy(app)

# Modelo de datos para SQLAlchemy
class Producto(db.Model):
    __tablename__ = 'productos'
    
    id = db.Column(db.Integer, primary_key=True)
    nombre = db.Column(db.String(100), nullable=False)
    precio = db.Column(db.Float, nullable=False)
    cantidad = db.Column(db.Integer, nullable=False)
    categoria = db.Column(db.String(50), nullable=False)
    fecha_registro = db.Column(db.DateTime, default=datetime.utcnow)
    
    def __repr__(self):
        return f'<Producto {self.nombre}>'
    
    def to_dict(self):
        return {
            'id': self.id,
            'nombre': self.nombre,
            'precio': self.precio,
            'cantidad': self.cantidad,
            'categoria': self.categoria,
            'fecha_registro': self.fecha_registro.strftime('%Y-%m-%d %H:%M:%S')
        }

# Rutas principales - UNA SOLA VEZ CADA UNA
@app.route('/')
def index():
    return render_template('index.html')

# 👇 SOLO UNA DEFINICIÓN DE contactos - ELIMINA CUALQUIER OTRA
@app.route('/contactos', methods=['GET', 'POST'])
def contactos():
    form = ContactoForm()
    return render_template('contactos.html', form=form)

# Rutas para manejo de archivos TXT
@app.route('/guardar-txt', methods=['POST'])
def guardar_txt():
    try:
        nombre = request.form['nombre']
        email = request.form['email']
        mensaje = request.form['mensaje']
        
        data = f"Nombre: {nombre}\nEmail: {email}\nMensaje: {mensaje}\nFecha: {datetime.now()}\n{'-'*50}\n"
        
        # Crear directorio data si no existe
        os.makedirs('inventario/data', exist_ok=True)
        
        with open('inventario/data/datos.txt', 'a', encoding='utf-8') as file:
            file.write(data)
        
        flash('Datos guardados en TXT exitosamente', 'success')
        return redirect(url_for('contactos'))
    except Exception as e:
        flash(f'Error al guardar en TXT: {str(e)}', 'error')
        return redirect(url_for('contactos'))

@app.route('/leer-txt')
def leer_txt():
    datos = []
    try:
        with open('inventario/data/datos.txt', 'r', encoding='utf-8') as file:
            contenido = file.read()
            # Procesar el contenido para mostrarlo
            lineas = contenido.split('\n')
            datos = [linea for linea in lineas if linea.strip()]
        return render_template('datos.html', datos=datos, formato='TXT')
    except FileNotFoundError:
        flash('No hay datos en el archivo TXT', 'warning')
        return render_template('datos.html', datos=[], formato='TXT')

# Rutas para manejo de archivos JSON
@app.route('/guardar-json', methods=['POST'])
def guardar_json():
    try:
        nombre = request.form['nombre']
        email = request.form['email']
        mensaje = request.form['mensaje']
        
        nuevo_registro = {
            'nombre': nombre,
            'email': email,
            'mensaje': mensaje,
            'fecha': datetime.now().strftime('%Y-%m-%d %H:%M:%S')
        }
        
        archivo_json = 'inventario/data/datos.json'
        os.makedirs('inventario/data', exist_ok=True)
        
        # Leer datos existentes o crear nueva lista
        try:
            with open(archivo_json, 'r', encoding='utf-8') as file:
                datos = json.load(file)
        except (FileNotFoundError, json.JSONDecodeError):
            datos = []
        
        datos.append(nuevo_registro)
        
        with open(archivo_json, 'w', encoding='utf-8') as file:
            json.dump(datos, file, indent=4, ensure_ascii=False)
        
        flash('Datos guardados en JSON exitosamente', 'success')
        return redirect(url_for('contactos'))
    except Exception as e:
        flash(f'Error al guardar en JSON: {str(e)}', 'error')
        return redirect(url_for('contactos'))

@app.route('/leer-json')
def leer_json():
    try:
        with open('inventario/data/datos.json', 'r', encoding='utf-8') as file:
            datos = json.load(file)
        return render_template('datos.html', datos=datos, formato='JSON')
    except FileNotFoundError:
        flash('No hay datos en el archivo JSON', 'warning')
        return render_template('datos.html', datos=[], formato='JSON')

# Rutas para manejo de archivos CSV
@app.route('/guardar-csv', methods=['POST'])
def guardar_csv():
    try:
        nombre = request.form['nombre']
        email = request.form['email']
        mensaje = request.form['mensaje']
        
        archivo_csv = 'inventario/data/datos.csv'
        os.makedirs('inventario/data', exist_ok=True)
        
        file_exists = os.path.isfile(archivo_csv)
        
        with open(archivo_csv, 'a', newline='', encoding='utf-8') as file:
            writer = csv.writer(file)
            if not file_exists:
                writer.writerow(['Nombre', 'Email', 'Mensaje', 'Fecha'])
            writer.writerow([nombre, email, mensaje, datetime.now().strftime('%Y-%m-%d %H:%M:%S')])
        
        flash('Datos guardados en CSV exitosamente', 'success')
        return redirect(url_for('contactos'))
    except Exception as e:
        flash(f'Error al guardar en CSV: {str(e)}', 'error')
        return redirect(url_for('contactos'))

@app.route('/leer-csv')
def leer_csv():
    datos = []
    try:
        with open('inventario/data/datos.csv', 'r', encoding='utf-8') as file:
            reader = csv.reader(file)
            for row in reader:
                datos.append(row)
        return render_template('datos.html', datos=datos, formato='CSV')
    except FileNotFoundError:
        flash('No hay datos en el archivo CSV', 'warning')
        return render_template('datos.html', datos=[], formato='CSV')

# Rutas para SQLAlchemy (CRUD completo)
@app.route('/productos')
def productos():
    productos = Producto.query.all()
    return render_template('productos.html', productos=productos)

@app.route('/producto/nuevo', methods=['GET', 'POST'])
def nuevo_producto():
    form = ProductoForm()
    if request.method == 'POST' and form.validate_on_submit():
        try:
            producto = Producto(
                nombre=form.nombre.data,
                precio=form.precio.data,
                cantidad=form.cantidad.data,
                categoria=form.categoria.data
            )
            db.session.add(producto)
            db.session.commit()
            flash('Producto guardado en SQLite exitosamente', 'success')
            return redirect(url_for('productos'))
        except Exception as e:
            flash(f'Error al guardar producto: {str(e)}', 'error')
            return redirect(url_for('nuevo_producto'))
    
    return render_template('producto_form.html', form=form, producto=None)

@app.route('/producto/editar/<int:id>', methods=['GET', 'POST'])
def editar_producto(id):
    producto = Producto.query.get_or_404(id)
    form = ProductoForm()
    
    if request.method == 'GET':
        # Llenar el formulario con los datos del producto
        form.nombre.data = producto.nombre
        form.precio.data = producto.precio
        form.cantidad.data = producto.cantidad
        form.categoria.data = producto.categoria
    
    if request.method == 'POST' and form.validate_on_submit():
        try:
            producto.nombre = form.nombre.data
            producto.precio = form.precio.data
            producto.cantidad = form.cantidad.data
            producto.categoria = form.categoria.data
            
            db.session.commit()
            flash('Producto actualizado exitosamente', 'success')
            return redirect(url_for('productos'))
        except Exception as e:
            flash(f'Error al actualizar producto: {str(e)}', 'error')
    
    return render_template('producto_form.html', form=form, producto=producto)

@app.route('/producto/eliminar/<int:id>')
def eliminar_producto(id):
    producto = Producto.query.get_or_404(id)
    try:
        db.session.delete(producto)
        db.session.commit()
        flash('Producto eliminado exitosamente', 'success')
    except Exception as e:
        flash(f'Error al eliminar producto: {str(e)}', 'error')
    
    return redirect(url_for('productos'))

@app.route('/leer-sqlite')
def leer_sqlite():
    productos = Producto.query.all()
    return render_template('datos.html', datos=productos, formato='SQLite')

# Crear tablas si no existen
with app.app_context():
    try:
        db.create_all()
        print("✅ Base de datos verificada/creada correctamente")
    except Exception as e:
        if "already exists" in str(e):
            print("ℹ️ Las tablas ya existen, continuando con la ejecución")
        else:
            print(f"❌ Error inesperado: {e}")

if __name__ == '__main__':
    app.run(debug=True)