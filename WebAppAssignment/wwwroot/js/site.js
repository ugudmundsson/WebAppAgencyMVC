﻿

// ---------------------OVERLAY Logout-------------------------------

document.addEventListener("DOMContentLoaded", function () {
    var logoutBtn = document.getElementById("logoutBtn");
    var closelogoutBtn = document.getElementById("closelogoutBtn");
    var logout = document.getElementById("logout-overlay");

    logoutBtn.addEventListener("click", function () {
        logout.style.display = "block";
    });

    closelogoutBtn.addEventListener("click", function () {
        logout.style.display = "none";
    });

    window.addEventListener("click", function (event) {
        if (event.target == logout) {
            logout.style.display = "none";
        }
    });
});



// ------------------OVERLAY Addproject----------------------------

document.addEventListener("DOMContentLoaded", function () {
    const imageSize = 150
    var openOverlayBtn = document.getElementById("openOverlayBtn");
    var closeOverlayBtn = document.getElementById("closeOverlayBtn");
    var overlay = document.getElementById("overlay");

    openOverlayBtn.addEventListener("click", function () {
        overlay.style.display = "block";
    });

    closeOverlayBtn.addEventListener("click", function () {
        overlay.style.display = "none";
    });

    window.addEventListener("click", function (event) {
        if (event.target == overlay) {
            overlay.style.display = "none";
        }
    });

    //handle image

    document.querySelectorAll('.image-previewer').forEach(previewer => {
        const fileInput = previewer.querySelector('input[type="file"]')
        const imagePreview = previewer.querySelector('.image-preview')

        previewer.addEventListener('click', () => fileInput.click())

        fileInput.addEventListener('change', ({ target: { files } }) => {
            const file = files[0]
            if (file)
                processImage(file, imagePreview, previewer, imageSize)

        })
    });

});

async function loadImage(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader()

        reader.onerror = () => reject(new Error("Failed."))
        reader.onload = (e) => {
            const img = new Image()
            img.onerror = () => reject(new Error("Failed."))
            img.onload = () => resolve(img)
            img.src = e.target.result
        }
        reader.readAsDataURL(file)
    })
}

async function processImage(file, imagePreview, previewer, imageSize = 150) {
    try {
        const img = await loadImage(file)
        const canvas = document.createElement('canvas')
        canvas.width = imageSize
        canvas.height = imageSize

        const ctx = canvas.getContext('2d')
        ctx.drawImage(img, 0, 0, imageSize, imageSize)
        imagePreview.src = canvas.toDataURL('image/jpeg')

    }
    catch (error){
        console.error('Failed on image-process', error)
    }
}

//------------ LOADING PROJECT STATUSES -----------------

function loadProjects(action) {
    const container = document.getElementById('project-list-container');
    fetch(`/Status/${action}`)
        .then(response => response.text())
        .then(html => {
            container.innerHTML = html;
            bindEditButton();
        })
        .catch(error => console.error('Error loading projects:', error));
}



//------------------- EDIT PROJECT ----------------


function bindEditButton() {
        document.querySelectorAll('[data-url]').forEach(button => {
            button.addEventListener('click', async function () {
                const url = this.getAttribute('data-url');
                const target = document.getElementById('filledit-overlay');
                const response = await fetch(url);
                const html = await response.text();
                target.innerHTML = html;

                const modal = target.querySelector('#edit-overlay');

                if (modal) {
                    modal.style.display = 'block';
                    

                }

                const closeBtn = target.querySelector('#closeeditBtn');
                closeBtn.addEventListener('click', function () {
                    modal.style.display = 'none';
                    target.innerHTML = '';
                });

                
            })
        })
    };



document.addEventListener('DOMContentLoaded', () => {
    loadProjects('All'); 
    bindEditButton();   
});
