function initMap(geojsonFeature) {
    var map = L.map('map').setView([14.5995, 120.9842], 10); // Manila, Philippines
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    L.geoJSON(geojsonFeature, {
        pointToLayer: function (feature, latlng) {
            var customIcon = L.icon({
                iconUrl: feature.properties.iconUrl,  // Use the iconUrl from the properties
                iconSize: [45, 45],
                iconAnchor: [10, 10]
            });
            return L.marker(latlng, { icon: customIcon });
        },
        onEachFeature: function (feature, layer) {
            if (feature.properties && feature.properties.popupContent) {
                layer.bindPopup(feature.properties.popupContent);
            }
        }
    }).addTo(map);
}