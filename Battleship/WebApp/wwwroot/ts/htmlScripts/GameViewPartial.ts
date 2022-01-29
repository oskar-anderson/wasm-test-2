export default class GameViewPartial {


    public static draw(board: number[], height: number, width: number) {
        let canvas = document.querySelector("canvas"); // Select our canvas element
        if (canvas === null) {
            throw new Error("Canvas was not found!");
        }
        let ctx = canvas.getContext("2d"); // Save the context we're going to use
        if (ctx === null) { throw new Error("Canvas ctx could not be gotten!"); }
        let scale = 1; // Scales the whole image by this amount, set to 1 for default size
    
        // Make sure the canvas is no larger than the size we need
        canvas.width = width * scale;
        canvas.height = height * scale;
        
        ctx.fillStyle = "#000";
        ctx.fillRect(0, 0, canvas.width, canvas.height);
        
        // Loop through each color and draw that section
        for (let row = 0; row < height; row++) {
            for (let col = 0; col < width; col++) { // Since there are nested arrays we need two for loops
                ctx.fillStyle = `rgba(
                        ${board[(row * width + col) * 4 + 0]},
                        ${board[(row * width + col) * 4 + 1]},
                        ${board[(row * width + col) * 4 + 2]},
                        ${board[(row * width + col) * 4 + 3]})`;  // Set the color to the one specified
                ctx.fillRect(col * scale, row * scale, scale, scale); // Actually draw the rectangle
    
            }
        }
    }
}