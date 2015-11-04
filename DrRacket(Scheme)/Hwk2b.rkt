;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-intermediate-lambda-reader.ss" "lang")((modname Hwk2b1) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #f #t none #f () #f)))
(require 2htdp/universe)
(require 2htdp/image)

(define-struct bt-node (val left right))
(define-struct bt-empty ())

(define (a-number digit)
     (text (number->string digit) 10 "black"))
; Signature: img-node n : n -> Image 
; Purpose: Drawing the nodes and place numbers inside.
(define (img-node n ncolor ccolor)
  (overlay (text (number->string n) 15 ncolor)
  (overlay (circle (+ (* (string-length (number->string n)) 6) 30) "outline" "black")
           (circle (+ (* (string-length (number->string n)) 6) 30) "solid" ccolor))))
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
    [(bt-node? bt) (connect-level (img-node (bt-node-val bt)"black" "beige")(img-bt (bt-node-left bt))(img-bt (bt-node-right bt)))]))


(define EX
  (make-bt-node
   4
    (make-bt-node
      1
        (make-bt-node 2 (make-bt-empty) (make-bt-empty))
        (make-bt-node 3 (make-bt-empty) (make-bt-empty)))
    (make-bt-empty)))
; Signature: tree, path list -> N
; Follows tree path to the root and returns the value at the end of the path
(define (value-at-path t p)
  (cond
    [(bt-empty? t)(error "path leaves tree")]
    [(empty? p) (bt-node-val t)]
    [(string=? "l" (first p)) (value-at-path (bt-node-left t) (rest p))]
    [else (value-at-path (bt-node-right t) (rest p))]))

; Signature: tree, path list -> boolean, string
; Follows tree path to root, if path ends on node of the tree returns true
; if its outside it returns false including. If its ends on "empty tree"
(define (path-in-tree? t p)
  (cond
     [(bt-empty? t) #false]
     [(empty? p) #true]
     [(string=? "l" (first p)) (path-in-tree? (bt-node-left t) (rest p))]
     [else (path-in-tree? (bt-node-right t) (rest p))]))

; Signature: tree, path list -> image
; Draws a bintree and changes the nodes that are on the path to "darkblue" 
(define (img-highlight-tree t p)
  (if (path-in-tree? t p)
      (cond
        [(bt-empty? t) (text "empty" 15 "darkblue")]
        [(empty? p) (connect-level (img-node (bt-node-val t) "white" "darkblue") (img-bt (bt-node-left t)) (img-bt (bt-node-right t)))]
        [(string=? "l" (first p)) (connect-level (img-node (bt-node-val t) "white" "darkblue") (img-highlight-tree (bt-node-left t) (rest p)) (img-bt (bt-node-right t)))]
        [(string=? "r" (first p)) (connect-level (img-node (bt-node-val t) "white" "darkblue") (img-bt (bt-node-left t)) (img-highlight-tree (bt-node-right t) (rest p)))])
      (error "path leaves tree")))


(value-at-path EX '())

(path-in-tree? EX '())


(img-highlight-tree EX (list "l" "r"))
