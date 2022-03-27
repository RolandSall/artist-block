import { PixelCrop } from 'react-image-crop'

// const TO_RADIANS = Math.PI / 180

export function cropPreview(
    image,
    canvas,
    crop,
    scale = 1,
    rotate = 0,
) {
    const ctx = canvas.getContext('2d')

    if (!ctx) {
        throw new Error('No 2d context')
    }

    const scaleX = image.naturalWidth / image.width
    const scaleY = image.naturalHeight / image.height
    const pixelRatio = window.devicePixelRatio || 1

    canvas.width = Math.floor(crop.width * pixelRatio * scaleX)
    canvas.height = Math.floor(crop.height * pixelRatio * scaleY)

    ctx.scale(pixelRatio, pixelRatio)
    ctx.imageSmoothingQuality = 'high'

    const cropX = crop.x * scaleX
    const cropY = crop.y * scaleY
    const cropWidth = crop.width * scaleX
    const cropHeight = crop.height * scaleY

    // const rotateRads = rotate * TO_RADIANS
    // const centerX = image.width / 2
    // const centerY = image.height / 2

    // ctx.save()
    // ctx.translate(centerX, centerY)
    // ctx.rotate(rotateRads)

    ctx.drawImage(image, cropX, cropY, cropWidth, cropHeight, 0, 0, cropWidth, cropHeight)

    // ctx.restore()
}
