document.addEventListener("DOMContentLoaded", function () {

    let page = 1;
    let seed = 123;
    let likes = 3.5;
    let lang = "en-US";
    let view = "table";
    let isThrottled = false; // Prevent double-fetching on scroll

    const tableDiv = document.getElementById("tableView");
    const galleryDiv = document.getElementById("galleryView");
    const likesInput = document.getElementById("likes");
    const likesValue = document.getElementById("likesValue");
    const seedInput = document.getElementById("seed");
    const langSelect = document.getElementById("lang");

    function updateLikesText() {
        likesValue.textContent = likes.toFixed(1);
    }

    function resetAndLoad() {
        seed = parseInt(seedInput.value || "123");
        lang = langSelect.value;
        likes = parseFloat(likesInput.value || "3.5");
        updateLikesText();

        page = 1;
        tableDiv.innerHTML = "";
        galleryDiv.innerHTML = "";

        loadSongs();
    }

    async function loadSongs() {
        try {
            const res = await fetch(`/api/songs?page=${page}&seed=${seed}&lang=${lang}&likes=${likes}`);
            if (!res.ok) throw new Error("Network response was not ok");
            
            const data = await res.json();
            
            // Check if items exist and aren't empty
            if (data.items && data.items.length > 0) {
                renderTable(data.items, page > 1);
                renderGallery(data.items, page > 1);
            }
        } catch (error) {
            console.error("Error fetching songs:", error);
        }
    }

    function renderTable(items, append = false) {
        if (!append) tableDiv.innerHTML = "";
        items.forEach(song => {
            const songLikes = song.likes ?? 0;
            const card = document.createElement("article");
            card.className = "song-card";
            card.innerHTML = `
                <div class="cover-col">${getInitials(song.title)}</div>
                <div class="title-col"><h3>${song.index ?? ''}. ${song.title}</h3></div>
                <div class="genre-col"><span>${song.genre}</span><span>${song.album}</span></div>
                <div class="likes-col">⭐ ${songLikes}</div>
            `;
            tableDiv.appendChild(card);
        });
    }

    function renderGallery(items, append = false) {
        if (!append) galleryDiv.innerHTML = "";
        items.forEach(song => {
            const songLikes = song.likes ?? 0;
            const card = document.createElement("article");
            card.className = "song-card";
            card.innerHTML = `
                <div class="cover-col">${getInitials(song.title)}</div>
                <div class="title-col"><h3>${song.title}</h3></div>
                <div class="genre-col"><span>${song.genre}</span><span>${song.album}</span></div>
                <div class="likes-col">⭐ ${songLikes}</div>
            `;
            galleryDiv.appendChild(card);
        });
    }

    function getInitials(text) {
        if (!text) return "??";
        return text
            .split(/\s+/)
            .filter(Boolean)
            .slice(0, 2)
            .map(part => part[0])
            .join("")
            .toUpperCase();
    }

    function randomSeed() {
        const randomValue = Math.floor(Math.random() * 100000);
        seedInput.value = randomValue;
        resetAndLoad();
    }

    function switchView() {
        view = view === "table" ? "gallery" : "table";
        tableDiv.style.display = view === "table" ? "grid" : "none";
        galleryDiv.style.display = view === "gallery" ? "grid" : "none";
    }

    // Optional Infinite Scroll implementation 
    window.addEventListener("scroll", () => {
        if (isThrottled) return;
        if ((window.innerHeight + window.scrollY) >= document.documentElement.scrollHeight - 100) {
            isThrottled = true;
            page++;
            loadSongs().then(() => {
                setTimeout(() => isThrottled = false, 200);
            });
        }
    });

    // Event Listeners
    likesInput.addEventListener("input", function () {
        likes = parseFloat(likesInput.value || "3.5");
        updateLikesText();
        resetAndLoad();
    });

    seedInput.addEventListener("input", resetAndLoad);
    langSelect.addEventListener("change", resetAndLoad);
    document.getElementById("randomSeedBtn").addEventListener("click", randomSeed);
    document.getElementById("viewToggle").addEventListener("click", switchView);

    // Initial setup execution
    updateLikesText();
    loadSongs();
});