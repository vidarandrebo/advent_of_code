use std::fs;
fn main() {
    let contents = fs::read_to_string("val.txt").expect("A string");
    let lines: Vec<i32> = contents
        .split_ascii_whitespace()
        .map(|x| x.parse::<i32>().unwrap())
        .collect();
    let mut increases = 0;
    for number in 1..lines.len() {
        if lines[number] > lines[number - 1] {
            increases += 1;
        }
    }
    println!("The number is {}", increases);
}
