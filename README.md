# TorrentNotifyBullet
Notify torrent downloads via PushBullet API

Using the API (https://docs.pushbullet.com/) provided by PushBullet it pushes a note to all your devices configured on PushBullet.

You need to obtain an access token from PushBullet, you can find it in your account settings page (https://www.pushbullet.com/account).

You pass the token as the first parameter and the torrent name or whatever text you would like to see in the pushed notification.


Supports both Windows and Linux (using mono, see mono-linux branch).
