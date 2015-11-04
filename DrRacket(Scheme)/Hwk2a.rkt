;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-intermediate-lambda-reader.ss" "lang")((modname Hwk2a) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #f #t none #f () #f)))
(require 2htdp/universe)
(require 2htdp/image)

(define-struct bt-node (val left right))
(define-struct bt-empty ())

(define (a-number digit)
     (text (number->string digit) 10 "black"))
; Signature: img-node n : n -> Image 
; Purpose: Drawing the nodes and place numbers inside.
(define (img-node n)
            (overlay (a-number n)(overlay (circle (+ (* (string-length (number->string n)) 6) 30) "solid" "beige")
                           (circle(+ (* (string-length (number->string n)) 6) 30) "outline" "black"))))
; Signature: child-line lwidth rwidth : lwidth -> image , rwidth -> image.
; Purpose: Drawing the lines.
(define (child-line lwidth rwidth)
   (add-line
   (add-line (rectangle (+ (+ lwidth rwidth) 50) 60 "solid" (color 255 255 255 0))
            (+ (/ (+ lwidth rwidth) 2) 25) 0 (/ lwidth 2) 60 "black")
   (+ (/ (+ lwidth rwidth) 2) 25) 0 (+ (+ (/ rwidth 2) lwidth) 50) 60 "black"))
; Signature: connect-level node-img left-subtree-img right-subtree-img : connect-level -> image.
; Purpose: This function will line up the lines.
(define (connect-level node-img left-subtree-img right-subtree-img)
  (above/align "middle" node-img (above/align "middle" (child-line (image-width left-subtree-img)(image-width right-subtree-img))
                          (beside/align "top" (beside/align "top" left-subtree-img (rectangle 50 0 "solid" "white")) right-subtree-img))))
; Signature: img-bt bt : bt -> String or bt -> image.
; Purpose: To draw the tree.
(define (img-bt bt)
  (cond
    [(bt-empty? bt) (text "empty" 14 "blue")]
    [(bt-node? bt) (connect-level (img-node (bt-node-val bt))(img-bt (bt-node-left bt))(img-bt (bt-node-right bt)))]))

(img-bt
 (make-bt-node
  pi
  (make-bt-node
   1
   (make-bt-node 20 (make-bt-empty)(make-bt-empty))
   (make-bt-node 3 (make-bt-empty)(make-bt-empty)))
   (make-bt-empty)))
