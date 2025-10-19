// Nav active state based on current path
(function() {
  const path = location.pathname.split('/').pop() || 'index.html';
  document.querySelectorAll('.site-nav .nav-link').forEach(link => {
    const linkPath = link.getAttribute('href');
    if (linkPath === path) link.classList.add('active');
  });
  // wire search to search.html?q=...
  const searchForm = document.querySelector('form.search');
  const input = searchForm && searchForm.querySelector('input[type="search"]');
  if (searchForm && input) {
    searchForm.addEventListener('submit', function(e) {
      e.preventDefault();
      const q = (input.value || '').trim();
      const url = 'search.html' + (q ? ('?q=' + encodeURIComponent(q)) : '');
      location.href = url;
    });
  }
})();

// Event delegation for like/save buttons
document.addEventListener('click', (e) => {
  const likeBtn = e.target.closest('.like-btn');
  const saveBtn = e.target.closest('.save-btn');
  const profileTab = e.target.closest('.profile-tab');
  const addBtn = e.target.closest('#compose-add-btn');
  const removeImageBtn = e.target.closest('.remove-image-btn');
  
  if (likeBtn) {
    const pressed = likeBtn.getAttribute('aria-pressed') === 'true';
    likeBtn.setAttribute('aria-pressed', String(!pressed));
  }
  if (saveBtn) {
    const pressed = saveBtn.getAttribute('aria-pressed') === 'true';
    saveBtn.setAttribute('aria-pressed', String(!pressed));
  }
  if (profileTab) {
    document.querySelectorAll('.profile-tab').forEach(t => { t.classList.remove('active'); t.setAttribute('aria-selected', 'false'); });
    profileTab.classList.add('active');
    profileTab.setAttribute('aria-selected', 'true');
    const targetId = profileTab.getAttribute('aria-controls');
    document.querySelectorAll('[role="tabpanel"]').forEach(p => p.hidden = p.id !== targetId);
  }
  if (addBtn) {
    const fileInput = document.getElementById('compose-file-input');
    if (fileInput) fileInput.click();
  }
  if (removeImageBtn) {
    const item = removeImageBtn.closest('.compose-image-item');
    if (item) item.remove();
  }
});

// Handle file input change to add image
document.addEventListener('change', (e) => {
  const fileInput = e.target.closest('#compose-file-input');
  if (!fileInput) return;
  const file = fileInput.files && fileInput.files[0];
  if (!file) return;
  
  const reader = new FileReader();
  reader.onload = () => {
    const container = document.getElementById('compose-images-container');
    if (!container) return;
    
    const item = document.createElement('div');
    item.className = 'compose-image-item';
    item.innerHTML = `
      <img src="${reader.result}" alt="Preview" />
      <button type="button" class="remove-image-btn" aria-label="Remove image">
        <i class="fas fa-times"></i>
      </button>
    `;
    container.appendChild(item);
  };
  reader.readAsDataURL(file);
  fileInput.value = '';
});


