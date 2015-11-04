#lang eopl

;Sig: N, list -> list
;Purpose: take a lsit and insert a number into the correct order
; and return a list with the number included
(define (insert-num n lst)
  (cond
    [(null? lst) (cons n '())]
    [(< n (car lst)) (cons n lst)]
    [else (cons (car lst) (insert-num n (cdr lst)))]))

;Sig: list -> list
;Purpose: Takes a list recurses over it and returns a new
;list that is sorted
(define (sort-num lst)
  (cond
   [(null? lst) '()]
   [(insert-num (car lst) (sort-num (cdr lst)))]))

;Sig: parameter, list -> list
;Purpose: Sorts a list so that it satisfies the in-order? for each element
;i.e. if in-order? is <= is will sort the list in ascending order
(define (sort-any in-order? lst)
  (cond
   [(null? lst) '()]
   [(insert-any in-order? (car lst) (sort-any in-order? (cdr lst)))]))

;Sig: in-order?, x, list -> list
(define (insert-any in-order? x lst)
  (cond
    [(null? lst) (cons x '())]
    [(in-order? x (car lst)) (cons x lst)]
    [else (cons (car lst) (insert-any in-order? x (cdr lst)))]))