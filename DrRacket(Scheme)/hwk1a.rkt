;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-intermediate-lambda-reader.ss" "lang")((modname hwk1a) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #f #t none #f () #f)))
(require 2htdp/image)
(require 2htdp/universe)
;--------------;
;    Part1     ;
;--------------;
; Signature: size->color x : x -> String 
; Purpose: This function will take x as parameter and assigning a number to string.
(define (size->color x)
  (cond
    ; setting up all required colors and assigning a number to each color.
    [(= x 1) "blue"]
    [(= x 2) "red"]
    [(= x 3) "green"]
    [(= x 4) "violet"]
    [(= x 5) "orange"]))
;--------------;
;    Part2     ;
;--------------;
; Signature: img-disk size : size -> N
; Purpose: Taking size as parameter and drawing the rectangles based on size entered. 
(define (img-disk size)
  (cond
    ; if size is larger than 1 and size less than 5, then draw rectangle.
    [(and (>= size 1)(<= size 5))
     ; drawing rectangle and adding 1 to each size then * by 40 to make a difference in sizing for each rectangle.
     (rectangle (* (+ size 1) 40) 25  "solid" (size->color size))]
    ; putting this condition to print nothing if size is less than 1 and larger than 5.
    [(or (< size 1)(> size 5)) ""]))
;--------------;
;    Part3     ;
;--------------;
; Signature: img-twr stack : stack -> List-of-N
; Purpose: returning a stack of rectrangles on top of each other.
(define (img-twr stack)
  (cond
    ; check if stack is empty? (circle 0 "solid" "black") is not doing anythign but we need that to avoid second argument error.
    [(null? stack) (circle 0 "solid" "black")]
    [else
     (above
       ; taking first element in stack.
       (img-disk (first stack))
       ; taking whats left in stack and doing it recursively until we reach base case
       (img-twr (rest stack)))]))
;--------------;
;    Part4     ;
;--------------;
; Signature: launch init-disk-stack : init-disk-stack -> List-of-N
; Purpose: popping a window that will contain the full stack of rectangles inside it, clicking on window will make the most top rectangle in stack disappear.
(define (launch init-disk-stack)
  (big-bang init-disk-stack
            (to-draw render)
            (on-mouse click)
            (on-key move)))

(define (render stack)
  (underlay/align "middle" "bottom"
                  (place-image (rectangle 20 200 "solid" "black") 200 300
                               (empty-scene 400 400 "pink"))(above (img-twr stack)(rectangle 400 25 "solid" "black"))))

(define (click init-disk-stack x y press)
  (if (and (mouse=? press "button-down")
           (not (empty? init-disk-stack)))
      (remove (first init-disk-stack) init-disk-stack)init-disk-stack))
;---------------;
;    Bonus1     ;
;---------------;
(define (move init-disk-stack ke)
  (cond
    [(key=? ke "1") (cons 1 (remove 1 init-disk-stack))]
    [(key=? ke "2") (cons 2 (remove 2 init-disk-stack))]
    [(key=? ke "3") (cons 3 (remove 3 init-disk-stack))]
    [(key=? ke "4") (cons 4 (remove 4 init-disk-stack))]
    [(key=? ke "5") (cons 5 (remove 5 init-disk-stack))]
    [else init-disk-stack]))

(launch (cons 1 (cons 2 (cons 3 (cons 4 (cons 5 '()))))))