from flask import Flask

app = Flask(__name__)

@app.route("/")
def index():
    return "Bienvenido al Sistema de Turnos – Clínica XYZ"

@app.route("/usuario/<nombre>")
def usuario(nombre):
    return f"Bienvenido, {nombre}. Tu turno está en proceso."

@app.route("/producto/<nombre>")
def producto(nombre):
    return f"Producto: {nombre} – disponible."

@app.route("/libro/<titulo>")
def libro(titulo):
    return f"Libro: {titulo} – consulta exitosa."

@app.route("/cita/<paciente>")
def cita(paciente):
    return f"Bienvenido {paciente}, tu cita está registrada."



if __name__ == "__main__":
    app.run(debug=True)

