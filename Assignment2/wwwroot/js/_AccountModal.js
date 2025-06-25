function openModal(url) {
    fetch(url)
        .then(response => response.text())
        .then(html => {
            document.getElementById('accountModalContent').innerHTML = html;
            const modal = new bootstrap.Modal(document.getElementById('accountModal'));
            modal.show();
        });
}
