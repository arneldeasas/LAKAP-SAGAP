import Swal from 'sweetalert2';
import Toastify from 'toastify-js';
import 'toastify-js/src/toastify.css';
import 'bootstrap';

window.Confirmation = async (title, icon, text) => {
    let result = await Swal.fire({
        title: title ?? '',
        text: text ?? 'Are you sure you want to proceed?',
        icon: icon ?? 'info',
        showCancelButton: true,
        confirmButtonText: 'Confirm',
        cancelButtonText: 'Cancel',
        reverseButtons: true
    });

    // Return true if confirmed, false if canceled
    return result.isConfirmed;
};

window.Toast = (type, message) => {
    let colors = {
        info: "linear-gradient(to right, #2196F3, #6EC1E4)",
        warning: "linear-gradient(to right, #FF9800, #FFC107)",
        error: "linear-gradient(to right, #F44336, #E57373)",
        success: "linear-gradient(to right, #4CAF50, #8BC34A)"
    };

    Toastify({
        text: message || "This is a toast",
        duration: 3000,
        close: true,
        gravity: "top",
        position: "right",
        style: {
            background: colors[type] || colors.info,
        },
        stopOnFocus: false,
    }).showToast();
};

Toastify({
    text:"This is a toast",
    duration: 3000,
    close: true,
    gravity: "top",
    position: "right",
    style: {
        background: colors[type] || colors.info,
    },
    stopOnFocus: false,
}).showToast();