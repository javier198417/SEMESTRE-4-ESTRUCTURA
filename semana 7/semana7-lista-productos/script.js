// 1. Datos iniciales
const productos = [
    { nombre: "Laptop", precio: 800, descripcion: "Potente para trabajo y estudio." },
    { nombre: "Mouse Óptico", precio: 20, descripcion: "Ergonómico y con luces RGB." },
    { nombre: "Teclado Mecánico", precio: 55, descripcion: "Switches rojos para escritura suave." }
];

// 2. Referencias a elementos del DOM
const listaUL = document.getElementById('lista-productos');
const btnAgregar = document.getElementById('btn-agregar');

// 3. Función para renderizar los productos
function renderizarProductos() {
    // Limpiamos la lista para evitar duplicados al refrescar
    listaUL.innerHTML = "";

    productos.forEach((producto) => {
        // Creamos la plantilla dinámica
        const item = `
            <li>
                <strong>${producto.nombre}</strong> - $${producto.precio}
                <p>${producto.descripcion}</p>
            </li>
        `;
        // Insertamos en el HTML
        listaUL.innerHTML += item;
    });
}

// 4. Función para agregar un nuevo producto
function agregarProducto() {
    const nombre = document.getElementById('nombre').value;
    const precio = document.getElementById('precio').value;
    const descripcion = document.getElementById('descripcion').value;

    // Validación básica
    if (nombre === "" || precio === "" || descripcion === "") {
        alert("Por favor, completa todos los campos");
        return;
    }

    // Crear el nuevo objeto y añadirlo al arreglo
    const nuevoProducto = {
        nombre: nombre,
        precio: precio,
        descripcion: descripcion
    };

    productos.push(nuevoProducto);

    // Volver a renderizar la lista y limpiar campos
    renderizarProductos();
    limpiarCampos();
}

function limpiarCampos() {
    document.getElementById('nombre').value = "";
    document.getElementById('precio').value = "";
    document.getElementById('descripcion').value = "";
}

// 5. Inicialización
btnAgregar.addEventListener('click', agregarProducto);
renderizarProductos(); // Renderiza los productos iniciales al cargar