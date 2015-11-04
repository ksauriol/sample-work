;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-intermediate-lambda-reader.ss" "lang")((modname hwk1b1) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #f #t none #f () #f)))
(require 2htdp/image)
(require 2htdp/universe)
	
(define-struct w(sel lef mid rig))
	
(define(inBound x)
  (if(and (not(null? x)) (and (>= x 1) (<= x 5))) #t #f))
	
(define (launch mainScene)
  (big-bang mainScene
            (to-draw draw-pegs)
            (on-mouse mouse-click)))
        
(define (draw-peg stack sel num)
  (underlay/align "middle" "bottom" (place-image (rectangle 20 200 "solid" "black") 150 125 (empty-scene 300 200 "pink"))
                  (above (img-twr stack sel (= sel num))(rectangle 250 25 "solid" "black"))))
	
(define (draw-pegs mainScene)
  (beside (draw-peg (w-lef mainScene) (w-sel mainScene) 1)
          (beside (draw-peg (w-mid mainScene)(w-sel mainScene) 2)(draw-peg(w-rig mainScene)(w-sel mainScene) 3))))

(define (sceneUpdate K newStack)
  (cond
    [(equal? (w-sel K)  newStack) (make-w -1 (w-lef K) (w-mid K) (w-rig K))]
    [(and (equal? (w-sel K) -1) (or (or (and (= newStack 1) (not(inBound (length(w-lef K)))))
                                   (and (= newStack 2) (not(inBound (length(w-mid K))))) ) (and (= newStack 3) (not(inBound (length(w-rig K))))))) K]
    [(= (w-sel K) -1) (make-w newStack (w-lef K) (w-mid K) (w-rig K))]
    [(and (equal? (w-sel K) 1) (equal? newStack 2))
     (make-w -1 (rest (w-lef K)) (cons (first (w-lef K)) (w-mid K))(w-rig K))]
    [(and (equal? (w-sel K) 1) (equal? newStack 3))
     (make-w -1 (rest (w-lef K))  (w-mid K) (cons (first (w-lef K))(w-rig K)))]
    [(and (equal? (w-sel K) 2) (equal? newStack 1))
     (make-w -1 (cons (first(w-mid K)) (w-lef K)) (rest (w-mid K))(w-rig K))]
    [(and (equal? (w-sel K) 2) (equal? newStack 3))
     (make-w -1 (w-lef K) (rest (w-mid K)) (cons (first (w-mid K)) (w-rig K)))]
    [(and (equal? (w-sel K) 3) (equal? newStack 1))
     (make-w -1 (cons (first (w-rig K)) (w-lef K)) (w-mid K) (rest (w-rig K)))]
    [(and (equal? (w-sel K) 3) (equal? newStack 2))
     (make-w -1 (w-lef K) (cons (first (w-rig K)) (w-mid K)) (rest (w-rig K)))]))
	
(define (mouse-click mainScene x y input)
  (if (and (mouse=? input "button-down") (not (empty? mainScene)))
      (cond
        [(< x 300) (sceneUpdate mainScene 1)]
        [(> x 600) (sceneUpdate mainScene 3)]
        [else (sceneUpdate mainScene 2)]) mainScene))

(define (size->color x)
  (cond
    [(= x 1) "blue"]
    [(= x 2) "red"]
    [(= x 3) "green"]
    [(= x 4) "violet"]))
        
(define (img-disk size)
  (cond
    [(and (>= size 1)(<= size 5))(rectangle (* (+ size 1) 40) 25  "solid" (size->color size))]
    [(or (< size 1)(> size 5)) ""]))
	
(define (img-twr stack sel top)
  (cond
    [(null? stack) (circle 0 "solid" "black")]
    [(or (= sel -1) (not top)) (above (img-disk (first stack)) (img-twr (rest stack) sel #f))]
    [else (above (above (img-disk (first stack))(rectangle 20 40 "solid" "black"))(img-twr (rest stack) sel #f))]))
	

(launch (make-w -1 (list 1 2 3 4) (list ) (list )))