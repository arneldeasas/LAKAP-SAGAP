import "./styles/styles.scss";
import "bootstrap";
import Swal from "sweetalert2";

window.Confirmation = async (title, icon, text) => {
  let result = await Swal.fire({
    title: title ?? "",
    text: text ?? "Are you sure you want to proceed?",
    icon: icon ?? "info",
    showCancelButton: true,
    confirmButtonText: "Confirm",
    cancelButtonText: "Cancel",
    reverseButtons: true,
  });

  return result.isConfirmed;
};
