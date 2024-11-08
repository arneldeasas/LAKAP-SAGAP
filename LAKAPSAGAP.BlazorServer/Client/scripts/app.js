import Swal from 'sweetalert2';
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