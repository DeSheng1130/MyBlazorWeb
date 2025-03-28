export async function saveMessage(id, title) {
    //alert("Save successfully!");
    await Swal.fire(
        'Info!',
        `Id : ${id}, Title : ${title} , save sucessfully!`,
        'success'
    );
}

export async function errorMessage() {
    //alert("Save error!");
    await Swal.fire({
        title: 'Error!',
        text: 'Do you want to continue!',
        icon: 'error',
        confirmButtonText: 'Cool'
    });
}