<!-- TABLE OF CONTENTS -->
## Table of Contents

<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <ul>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
  </ol>
</details>


<!-- ABOUT THE PROJECT -->
## About The Project

Prototype of a whiteboarding tool. The final version was submitted and implemented into a VR input system and is not public. 

Controls:<br />
WASD: Move<br />
Right Click Hold: Turn Camera<br />
Left Click Hold: Grab / Connect anchor points<br />
Q: Grab sticky note and hover over whiteboard then press Q to cast it onto board<br />
<br />

Features:<br />
-Spawn Whiteboard<br />
-Spawn PostIt Note with custom note<br />
-UI prompt for text input<br />
-Click and drag<br />
-Anchor points that create lines between thought objects<br />
-Anchor points update on drag in 3D space<br />
-Casting post it note onto whiteboard<br />
-Resizing post it and whiteboard<br />
-Resize post it when whiteboard is being resized<br />
-Emoji buttons<br />


### Built With

* Unity3D API

On the unreleasable version, I used these tools as well:
* Photon Networking
* VR player controller

<!-- GETTING STARTED -->
### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/your_username_/WhiteboardProject.git
   ```
2. Press Play
   

<!-- USAGE EXAMPLES -->
## Usage

As you can see there are emojis, text, anchors for connecting, a floating UI, and boards.
![Components](https://github.com/ryandaepark/WhiteboardProject/assets/57121651/39820255-bf61-490d-9bb3-12ed2558ef6a)
![CreatePostIt](https://github.com/ryandaepark/WhiteboardProject/assets/57121651/11493dc8-ed48-4085-af06-817c1f11182b)

Below shows how it looks once the post its are placed into a thought map. 
![ThoughtMap](https://github.com/ryandaepark/WhiteboardProject/assets/57121651/de158446-0aec-4ad4-8e88-1d9c7d026b60)

This shows that the post its can be visualized in 3D but not all post its need to be attached to the board. 
![Casting](https://github.com/ryandaepark/WhiteboardProject/assets/57121651/42d39db0-8480-4e16-bcef-97f34b0a44f6)



