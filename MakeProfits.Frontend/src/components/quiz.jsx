import React, { useState } from 'react';
import './quiz.css';
import { useNavigate } from 'react-router-dom';
export default function Quiz() {
    const navigate = useNavigate();
	const questions = [
		{
			questionText: 'What is your income range',
			answerOptions: [
				{ answerText: '10k-40k', risk: 25 },
                { answerText: '40k-60k', risk: 20 },
                { answerText: '60k-90k', risk: 15 },
                { answerText: '90k-120k', risk: 10 }          
				
			],
		},
        {
			questionText: 'What is your age',
			answerOptions: [
				{ answerText: '60-90', risk: 25 },
                { answerText: '45-60', risk: 20 },
                { answerText: '30-45', risk: 15 },
                { answerText: '18-30', risk: 10 }          
				
			],
           
		},
        {
            questionText: 'What is your overall debt',
            answerOptions: [
                { answerText: '5l-10+l', risk: 25 },
                { answerText: '3l-5l', risk: 20 },
                { answerText: '1l-3l', risk: 15 },
                { answerText: '<1l', risk: 10 }          
                
            ],
        }
	];

	const [currentQuestion, setCurrentQuestion] = useState(0);
	const [showScore, setShowScore] = useState(false);
	const [score, setScore] = useState(0);

	const handleAnswerOptionClick = (risk) => {
		
			setScore(score + risk);
		

		const nextQuestion = currentQuestion + 1;
		if (nextQuestion < questions.length) {
			setCurrentQuestion(nextQuestion);
		} else {
			setShowScore(true);
		}
	};
	return (
        <div className='quiz-body'>

        
		<div className='container-quiz'>
			{showScore ? (
				<div className='score-section'>
					You risk {score}
                    <button onClick={() => navigate('/Alladvisor')}>next step</button>
				</div>
			) : (
				<>
					<div className='question-section'>
						<div className='question-count'>
							<span>Question {currentQuestion + 1}</span>/{questions.length}
						</div>
						<div className='question-text'>{questions[currentQuestion].questionText}</div>
					</div>
					<div className='answer-section'>
						{questions[currentQuestion].answerOptions.map((answerOption) => (
							<button onClick={() => handleAnswerOptionClick(answerOption.risk)}>{answerOption.answerText}</button>
						))}
					</div>
				</>
			)}
		</div>
        </div>
	);
}