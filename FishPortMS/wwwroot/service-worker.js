// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
self.addEventListener('fetch', () => { });

self.addEventListener('push', function (e) {
    var payload;

    if (e.data) {
        try {
            payload = e.data.json();
        } catch (error) {
            console.error('Error parsing push notification data:', error);
            payload = { message: 'Standard Message' };
        }
    } else {
        payload = { message: 'Standard Message' };
    }

    var options = {
        body: payload.message,
        icon: 'icon-192.png',
        vibrate: [100, 50, 100],
        data: {
            dateOfArrival: Date.now(),
            url: payload.url
        },
        actions: [
            {
                action: 'explore', title: 'Go interact with this!',
                icon: 'Media/PushNotification/tap.png'
            },
            {
                action: 'close', title: 'Ignore',
                icon: 'Media/PushNotification/cross.png'
            },
        ]
    };

    e.waitUntil(
        self.registration.showNotification("New Notification!", options)
            .catch(error => console.error('Error displaying notification:', error))
    );
});

self.addEventListener('notificationclick', function (e) {
    var notification = e.notification;
    var action = e.action;

    var promiseChain;

    if (action === 'close') {
        notification.close();
        promiseChain = Promise.resolve();
    } else {
        promiseChain = clients.openWindow(notification.data.url);
        notification.close();
    }

    e.waitUntil(promiseChain);
});

self.addEventListener('message', event => {
    if (event.data?.type === 'SKIP_WAITING') self.skipWaiting();
});
