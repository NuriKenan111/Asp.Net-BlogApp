var tags = document.querySelectorAll('.tag');

tags.forEach(function(tag) {
    tag.addEventListener('mouseover', function() {
        tag.classList.add('active');
        if (tag.style.backgroundColor === 'blue') {
            
            var randomColor = getRandomColor();
            tag.style.backgroundColor = randomColor;
        }
    });

    tag.addEventListener('mouseout', function() {
        tag.classList.remove('active');
    });
    
});

function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}
