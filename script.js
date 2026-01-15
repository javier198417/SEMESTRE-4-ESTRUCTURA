const form = document.getElementById('registrationForm');
const submitBtn = document.getElementById('submitBtn');

// Objeto para rastrear la validez de cada campo
const fields = {
    nombre: false,
    email: false,
    password: false,
    confirm: false,
    edad: false
};

// Expresiones Regulares
const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
const passRegex = /^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,}$/;

// Función para mostrar/ocultar error y cambiar estilos
const validateField = (input, condition, errorId) => {
    const errorSpan = document.getElementById(errorId);
    if (condition) {
        input.classList.add('valid');
        input.classList.remove('invalid');
        errorSpan.style.display = 'none';
        return true;
    } else {
        input.classList.add('invalid');
        input.classList.remove('valid');
        errorSpan.style.display = 'block';
        return false;
    }
};

// Listeners para validación en tiempo real
form.nombre.addEventListener('input', (e) => {
    fields.nombre = validateField(e.target, e.target.value.length >= 3, 'errorNombre');
    checkForm();
});

form.email.addEventListener('input', (e) => {
    fields.email = validateField(e.target, emailRegex.test(e.target.value), 'errorEmail');
    checkForm();
});

form.password.addEventListener('input', (e) => {
    fields.password = validateField(e.target, passRegex.test(e.target.value), 'errorPassword');
    // Re-validar confirmación si ya tiene texto
    if(form.confirmPassword.value) {
        fields.confirm = validateField(form.confirmPassword, form.confirmPassword.value === e.target.value, 'errorConfirm');
    }
    checkForm();
});

form.confirmPassword.addEventListener('input', (e) => {
    fields.confirm = validateField(e.target, e.target.value === form.password.value, 'errorConfirm');
    checkForm();
});

form.edad.addEventListener('input', (e) => {
    fields.edad = validateField(e.target, parseInt(e.target.value) >= 18, 'errorEdad');
    checkForm();
});

// Habilitar botón si todo es true
function checkForm() {
    const isFormValid = Object.values(fields).every(val => val === true);
    submitBtn.disabled = !isFormValid;
}

// Manejo del envío
form.addEventListener('submit', (e) => {
    e.preventDefault();
    alert('✅ ¡Formulario enviado con éxito!');
    form.reset();
    resetStyles();
});

// Limpiar estilos al reiniciar
function resetStyles() {
    document.querySelectorAll('input').forEach(input => {
        input.classList.remove('valid', 'invalid');
    });
    document.querySelectorAll('.error').forEach(span => span.style.display = 'none');
    Object.keys(fields).forEach(key => fields[key] = false);
    submitBtn.disabled = true;
}